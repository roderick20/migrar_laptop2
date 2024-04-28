//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const uuid = require('uuid');

module.exports = (app) => {
    var mensajes = app.models.mensajes;
    app.get('/admin/messages', async (req, res) => {
        let messlist = await mensajes.list();
        res.render('mensajes/index', {
            title: 'Mensajes',
            layout: './shared/layout_admin.ejs',
            messlist: messlist.result.recordset,

        });
    });
    //Save message
    app.post('/admin/messages/save', async (req, res) => {
        const Name= req.body.Name;
        const Email= req.body.Email;
        const Subject= req.body.Subject;
        const Body= req.body.Body;
        const Phone= req.body.Phone;
        try {
        await mensajes.mensajePost(Name,Email,Subject, Body, Phone);
        res.redirect('/admin/messages');
        }catch (error) {
            res.status(500).send('Error al enviar el formulario');
        }
    });

    //Details
    app.get('/admin/messages/:UniqueId', async (req, res) => {
        let messlist = await mensajes.mensajeId(req.params.UniqueId);
        res.render('mensajes/mensajes', {
            title: 'Mensajes',
            layout: './shared/layout_admin.ejs',
            messlist: messlist.result.recordset,

        });
    });
}