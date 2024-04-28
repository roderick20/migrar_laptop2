require('dotenv').config();
const moment = require('moment');
const express = require('express');
const ejs = require('ejs');
const expressLayouts = require('express-ejs-layouts');
const expressSession = require('express-session');
const passport = require('passport');
const LocalStrategy = require('passport-local').Strategy;
const flash = require('connect-flash');
const { MongoClient } = require('mongodb');
const ObjectId = require('mongodb').ObjectId;
const consign = require('consign');

const app = express();
app.set('views', 'views');
app.set('view engine', 'ejs');
app.set('layout', './shared/layout');
app.use(expressLayouts);
app.locals.moment = moment;
app.use(express.static('public'));

app.use(express.json({ limit: '10mb' }));
app.use(express.urlencoded({ limit: '10mb', extended: true }));
//------------------
app.use(expressSession({ secret: 'your-secret-key', resave: true, saveUninitialized: true }));
app.use(passport.initialize());
app.use(passport.session());
app.use(flash());
app.use((req, res, next) => {
  res.locals.messages = req.flash();
  next();
});




app.use((req, res, next) => {
  res.locals.username = "";
  let urlAnonymous = ['/login'];
  const urlFound = urlAnonymous.find(item => req.originalUrl.toLowerCase().startsWith(item.toLowerCase()));
  if (urlFound == undefined) {
    if (req.isAuthenticated()) {
      console.log(req.session.passport.user.id);
      res.locals.username = req.session.req.session.passport.user.useName;
      res.locals.userid = req.session.userId;
      return next();
    }
    res.redirect('/login');
    return;
  }
  next();
});


//-----------------



// Middleware
app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(expressSession({ secret: 'your-secret-key', resave: true, saveUninitialized: true }));

consign()
    .include('controllers')
    .into(app);






const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}`);
});