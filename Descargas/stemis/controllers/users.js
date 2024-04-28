//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const uuid = require('uuid');

module.exports = (app) => {
    var users = app.models.users;
    //List Users
    app.get('/admin/users', async (req, res) => {
        let userslist = await users.list();
        res.render('users/index', {
            title: 'Users',
            layout: './shared/layout_admin.ejs',
            userslist: userslist.result.recordset,
        });
    });
    //Form user
    app.get('/admin/users/create', async (req, res) => {
        let userslist = await users.list();
        res.render('users/create', {
            title: 'Users',
            layout: './shared/layout_admin.ejs',
            userslist: userslist.result.recordset,
        });
    });
    //Save user
    app.post('/admin/users/save', async (req, res) => {
        const { Name, Email, Password, Enabled } = req.body;

        try {
        await users.usersCreate(Name,Email,Password,Enabled);
        res.redirect('/admin/users');
        }catch (error) {
            res.status(500).send('Error al crear el usuario');
        }
    });
    //Edit Users
    app.post('/admin/users/update/:UniqueId', async(req, res) =>{
        const UniqueId = req.params.UniqueId;
        const { Name, Email, Password, Enabled } = req.body;
        try {
            await users.usersUpdate(UniqueId, Name, Email, Password, Enabled);
            res.redirect('/admin/users');
        } catch (error) {
            res.status(500).send('Error al actualizar el usuario');
        }
    });
    //Users Data
    app.get('/admin/users/edit/:UniqueId', async(req, res) =>{
        let userslist = await users.UsersId(req.params.UniqueId)
        res.render('users/update', {
            title: 'Users',
            layout: './shared/layout_admin.ejs',
            userslist: userslist.result.recordset
        });
    });
    //Delete user
    app.delete('/admin/users/delete/:UniqueId', async (req, res) => {
        const UniqueId = req.params.UniqueId;
        try {
            await users.usersDelete(UniqueId);
            res.sendStatus(204); // Indicando que la eliminación se realizó correctamente
        } catch (error) {
            res.status(500).send('Error al eliminar el usuario');
        }
    });

    

    
}