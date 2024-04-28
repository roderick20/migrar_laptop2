require('dotenv').config();
const express = require('express');
const bodyParser = require('body-parser');
const cookieParser = require('cookie-parser');
const ejs = require('ejs');
const expressLayouts = require('express-ejs-layouts');
const session = require('express-session');
const consign = require('consign');

const app = express();
app.set('views', 'views');
app.set('view engine', 'ejs');
app.set('layout', './shared/layout');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(expressLayouts);
//app.use(express.static('uploads'));
app.use(express.static('public'));


app.use('/uploads', express.static(__dirname + '/uploads'));


app.use(session({
    secret: process.env.SESSION_SECRET,
    saveUninitialized: false,
    cookie: { maxAge: 1000 * 60 * 60 * 24 },
    resave: false
}));
//app.use(cookieParser("cookie-parser-secret"));
app.use(cookieParser(process.env.SESSION_SECRET));


app.use((req, res, next) => {
    // res.locals.username = ''; 
    // if (req.url != '/') {
    //     if (req.session.user == null) {
    //         res.redirect('/');
    //     }
    //     else{
    //         res.locals.username = req.session.user.userName;           
    //     }
    // }
    res.locals.username = 'Roderick';
    next();
});


consign()
    .include('models')
    .then('controllers')
    .into(app);

app.use(function (req, res, next) {
    res.status(404);
    if (req.accepts('html')) {    //Respuesta html
        res.render('404', { title: 'Página no encontrada' });
        return;
    }
    else if (req.accepts('json')) {// Respuesta json
        res.send({ error: 'Not found' });
        return;
    }
});

app.use((err, req, res, next) => {
    res.status(500);
    if (req.accepts('html')) {    //Respuesta html
        res.render('500', { title: 'Ocurrió un error en el servidor', error: 'Ocurrió un error en el servidor : ' + err });
        return;
    }
    else if (req.accepts('json')) {// Respuesta json
        res.send({ error: 'Ocurrió un error en el servidor : ' + err });
        return;
    }
});

const port = process.env.PORT || 3000;

app.listen(port, () => {
    console.log('Aplicación iniciada en el puerto 3000');
});
