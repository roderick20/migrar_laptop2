//v1.0.0
const modelLogin = require('../models/login');
const userModel = require('../models/user');
const nodemailer = require('nodemailer');
const fs = require('fs');

const passport = require('passport');
const LocalStrategy = require('passport-local').Strategy;

function codeEmail(length) {
    const caracteres = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let codigo = '';

    for (let i = 0; i < length; i++) {
        const indiceAleatorio = Math.floor(Math.random() * caracteres.length);
        codigo += caracteres.charAt(indiceAleatorio);
    }

    return codigo;
}

module.exports = (app) => {

    app.get('/login', (req, res) => {
        const successMessage = req.query.success;
        const errorMessage = req.query.error;
        res.render('auths/login', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            successMessage,
            errorMessage,
            error: ''
        });
    });

    passport.use(new LocalStrategy(
        async (username, password, done) => {
            const loginResult = await modelLogin.login(username, password);

            if (loginResult.result) {
                if (loginResult.result.recordsets[0].length > 0) {
                    const user = loginResult.result.recordsets[0][0];
                    return done(null, user);
                }
                else if (loginResult.result.recordsets[1].length > 0) {
                    const user = loginResult.result.recordsets[1][0];
                    return done(null, user);
                }
            }
            return done(null, false, { message: 'Credenciales inválidas' });
        }
    ));

    passport.serializeUser((user, done) => {
        done(null, { id: user.Id, name: user.Name, lastname: user.LastName});
    });

    passport.deserializeUser((user, done) => {
        done(null, { user });
    });

    app.post('/login', (req, res, next) => {

        passport.authenticate('local', (err, user, info) => {
            if (err) {
                return next(err);
            }
            if (!user) {
                return res.redirect('/login'); 
            }
            req.logIn(user, (err) => {
                // if (err) {
                //     return next(err);
                // }
                // if (user.hasOwnProperty('FirstName')) {
                //     return res.redirect('/persons');
                // } else {
                    return res.redirect('/teacher');
                // }
            });
        })(req, res, next);
    });

    app.get('/logout', (req, res) => {
        req.logout((err) => {
            if (err) {
                return next(err);
            }
            res.redirect('/login');
        });
    });

    //-----------------------------------------------------------------------------
    
    app.get('/register/:menssage?', async (req, res) => {
        res.render('auths/register', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            error: ''
        });
    });

    app.post('/register', async (req, res) => {
        let code = codeEmail(16);
        req.body.codeAuth = code;

        let login = await userModel.create(req.body);
        /*if (login.error != '') {
            res.redirect('/login/register' + login.error.replace(' ', '%20'));
            return;
        }*/

        const transportOptions = {
            host: 'smtp.office365.com',
            port: 587,
            secure: false,
            auth: {
              user: 'admpostgrado@ucsm.edu.pe',
              pass: 'EscuelaPostgrado$987',
            },
          };
        
         const transporter = nodemailer.createTransport(transportOptions);
         let dataTpl = fs.readFileSync('./public/tpl_email/tpl_register.html', 'utf8');
         dataTpl = dataTpl.replaceAll('%%code%%', code);
        
        
        const sender = {
            name: 'EPG - UCSM',
            address: 'info@agilex.pe',
        };
        
        const mailOptions = {
            from: sender,
            to: req.body.email,
            subject: 'Registro - Corngirmacion de cuenta',
            html: dataTpl,
            attachments: [
                {
                    filename: 'imagen1.png',
                    path: './public/tpl_email/logo.png',
                    cid: 'imagen1@correo.com',
                },
            ]
        };
        
        transporter.sendMail(mailOptions, (error, info) => {
        console.log(error);
        });

        res.redirect('/registermsg');
    });

    app.get('/registermsg', async (req, res) => {
        res.render('auths/registermsg', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            error: ''
        });
    });

    app.get('/registermsg2', async (req, res) => {
        res.render('auths/registermsg2', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            error: ''
        });
    });

    app.get('/confirmaremail/:codeAuth', async (req, res) => {
        let login = await userModel.validarCuenta(req.params.codeAuth);
        res.render('auths/confirmaremail', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            //message: login == 1 ? 'Cuenta validada' : "Error en la validación",
            message: 'Cuenta validada' 
        });
    });

    app.get('/recoverypassword/:menssage?', async (req, res) => {

        res.render('auths/recovery_password', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            message: ''
        });
    });

    app.post('/recoverypassword/', async (req, res) => {
        let code = codeEmail(8);
        req.body.password = code;
        let login = await userModel.recoverypassword(req.body);

        // if (login.error != '') {
        //     res.redirect('/recoverypassword/register' + login.error.replace(' ', '%20'));
        //     return;
        // }

        const transportOptions = {
            host: 'smtp.office365.com',
            port: 587,
            secure: false,
            auth: {
              user: 'admpostgrado@ucsm.edu.pe',
              pass: 'EscuelaPostgrado$987',
            },
          };
        
         const transporter = nodemailer.createTransport(transportOptions);
         let dataTpl = fs.readFileSync('./public/tpl_email/tpl_recoverypassword.html', 'utf8');
         dataTpl = dataTpl.replaceAll('%%code%%', code);
        
        
        const sender = {
            name: 'EPG - UCSM',
            address: 'info@agilex.pe',
        };
        
        const mailOptions = {
            from: sender,
            to: req.body.email,
            subject: 'Recueprar contraseña',
            html: dataTpl,
            attachments: [
                {
                    filename: 'imagen1.png',
                    path: './public/tpl_email/logo.png',
                    cid: 'imagen1@correo.com',
                },
            ]
        };
        
        transporter.sendMail(mailOptions, (error, info) => {
        console.log(error);
        });

        res.redirect('/registermsg2');
    });
}