//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const { v4: uuidv4 } = require('uuid');
const moment = require('moment');


module.exports = (app) => {
    var settings = app.models.settings;

    app.get('/admin/settings', async (req, res) => {
        let settingslistConf = await settings.listConf();
        let settingslistImage = await settings.listImage();
        let settingslistStyle = await settings.listStyle();
        res.render('settings/index', {
            title: 'Settings',
            layout: './shared/layout_admin.ejs',
            settingslistConf: settingslistConf.result.recordset,
            settingslistImage: settingslistImage.result.recordset,
            settingslistStyle: settingslistStyle.result.recordset,
        });
    });
    
    //Settings Data
    app.get('/admin/settings/edit/:UniqueId', async(req, res) =>{
        let settingslist = await settings.listbyId(req.params.UniqueId)
        res.render('settings/update', {
            title: 'Settings',
            layout: './shared/layout_admin.ejs',
            settingslist: settingslist.result.recordset,
        });
    });
    app.get('/admin/settings/editImage/:UniqueId', async(req, res) =>{
        let settingslist = await settings.listbyId(req.params.UniqueId)
        res.render('settings/update', {
            title: 'Settings',
            layout: './shared/layout_admin.ejs',
            settingslist: settingslist.result.recordset,
        });
    });
    //Update Settings
    app.post('/admin/settings/update/:UniqueId', async(req, res) =>{
        const UniqueId = req.params.UniqueId;
        const { Value} = req.body;
        try {
            await settings.settingUpdate(UniqueId, Value);
            res.redirect('/admin/settings');
        } catch (error) {
            res.status(500).send('Error al actualizar el usuario');
        }
    });



}