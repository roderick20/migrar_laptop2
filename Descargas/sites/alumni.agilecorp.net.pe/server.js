const express = require('express');
const app = express();

const cookieParser = require('cookie-parser');
const expressLayouts = require('express-ejs-layouts')
const expressSession = require('express-session');
const bp = require('body-parser');
const consign = require('consign');
const passport = require('passport');
const LocalStrategy = require('passport-local').Strategy;
const flash = require('connect-flash');
require('dotenv').config();

app.set('views', 'views');
app.set('view engine', 'ejs');
app.set('layout', './shared/layout');

app.use(expressLayouts);
app.use(express.static('public'));
app.use('/uploads', express.static('uploads'));

app.use(bp.urlencoded({ extended: true }))
app.use(cookieParser("cookie-parser-secret"));
app.use(express.json());
app.use(expressSession({ secret: 'thisismysecrctekeyfhrgfgrfrty84fwir767', resave: true, saveUninitialized: true }));
app.use(passport.initialize());
app.use(passport.session());
app.use(flash());

// app.use((req, res, next) => {
//     try {
//         res.locals.user = req.session.user;
//     } catch {
//         res.locals.user = {
//             username: 'Roderick'
//         };
//     }
//     next();
// });
app.use((req, res, next) => {
    res.locals.messages = req.flash();
    next();
});
app.use((req, res, next) => {
    res.locals.username = "";
    res.locals.userId = "";
    res.locals.role = "";
    res.locals.firstname = "";
    let urlAnonymous = ['/logout', '/login', '/personcard', '/personcardqr', '/downloadVCF'];
    if (req.originalUrl != '/') {
        console.log("---" + req.originalUrl);
        const urlFound = urlAnonymous.find(item => req.originalUrl.startsWith(item) && item != '/');
        console.log("+++" + urlFound);
        
        if (urlFound == undefined) {
            if (req.isAuthenticated()) {
                console.log(req.session.passport.user.role)
                res.locals.username = req.session.passport.user.name; // Aquí corregí 'req.session.req.session' a 'req.session.passport'
                res.locals.firstname = req.session.passport.user.FirstName; 
                res.locals.userid = req.session.passport.user.id; // Corregí 'req.session.userId' a 'req.session.passport.user.id'
                res.locals.role = req.session.passport.user.role;
                return next();
            } else {
                res.redirect('/login');
                return;
            }
        }
    } else {
        if (req.isAuthenticated()) {
            console.log(req.session.passport.user.id);
                res.locals.username = req.session.passport.user.user.name; // Aquí corregí 'req.session.req.session' a 'req.session.passport'
                res.locals.userid = req.session.passport.user.id; // Corregí 'req.session.userId' a 'req.session.passport.user.id'
            // Aquí puedes redirigir a otra página si el usuario ya está autenticado en lugar de "/login"
            // res.redirect('/dashboard');
        } else {
            res.redirect('/login');
            return;
        }
    }

    next();
});


const oneDay = 1000 * 60 * 60 * 24;
// app.use(session({
//     secret: "thisismysecrctekeyfhrgfgrfrty84fwir767",
//     saveUninitialized: true,
//     cookie: { maxAge: oneDay },
//     resave: false
// }));

consign()
        .include('models')
        .then('controllers')
        .into(app);

const port = process.env.PORT || 3000;

app.listen(port, () => {
    console.log('Aplicación iniciada en el puerto 3000');
});
