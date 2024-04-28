const { Console } = require('console');
const crypto = require('crypto');
const multer = require('multer');
const fs = require('fs');
const { v4: uuidv4 } = require('uuid');
const QRCode = require('qrcode');

const mascotaModel = require('../models/mascota');

//v1.0.0
module.exports = (app) => {


    app.get('/mascotas', async (req, res) => {
        try {
            res.render('mascotas/index', {
                title: 'Mascotas',
                list: await mascotaModel.list(req.session.passport.user.id)
            });
        }
        catch (ex) {

        }
    });

    app.get('/mascotas/create', async (req, res) => {
        try {
            res.render('mascotas/create', {
                title: 'Mascotas',
            });
        }
        catch (ex) {

        }
    });

    app.post('/mascotas/create', async (req, res) => {
        try {
            await mascotaModel.create(req.body, req.session.passport.user.id);            
            res.redirect(`/mascotas`);
        } catch (error) {
        }
    });


    //    //create


    //     app.get('/teacher', async (req, res) => {
    //         console.log('adfsngsaqui va el user iddfgui ', req.session.passport.user.id)
    //         try {
    //             const list = await mascotas.findAllId(req.session.passport.user.id)
    //             //console.log('adfsngsdfgui',list.result.recordset)
    //             res.render('mascotas/index', {
    //                 title: 'Formulario',
    //                 teacher: list.result.recordset[0]
    //             });
    //         }
    //         catch (ex) {
    //             res.render('mascotas/index', {
    //                 title: 'Formulario',
    //                 error: ex
    //             });
    //         }
    //     });

    //     app.get('/updateTeacher', async (req, res) => {
    //         console.log('adfsngsaqui va el user iddfgui ', req.session.passport.user.id)
    //         try {
    //             const list = await mascotas.findAllId(req.session.passport.user.id)
    // console.log(list);
    //             res.redirect('/mascotas/update/'+list.result.recordset.UniqueId);
    //             //console.log('adfsngsdfgui',list.result.recordset)
    //             // res.render('mascotas/index', {
    //             //     title: 'Formulario',
    //             //     teacher: list.result.recordset[0]
    //             // });
    //         }
    //         catch (ex) {
    //             res.render('mascotas/index', {
    //                 title: 'Formulario',
    //                 error: ex
    //             });
    //         }
    //     });

    //     app.get('/mascotas/update/:UniqueId', async (req, res) => {
    //         try {
    //             const list = await mascotas.findAllId(req.session.passport.user.id)
    //             res.render('mascotas/update', {
    //                 title: 'Formulario',
    //                 list: list.result.recordset
    //             });
    //         }
    //         catch (ex) {
    //             res.render('mascotas/update', {
    //                 title: 'Formulario',
    //                 error: ex
    //             });
    //         }
    //     });

    //     app.post('/mascotas/update/:UniqueId', async (req, res) => {
    //         try {
    //             await mascotas.update(req.params.UniqueId, req.body);
    //             res.redirect(`/updateTeacher`);
    //         } catch (error) {

    //         }
    //     });





}