//v1.0.0
const passport = require('passport');
const LocalStrategy = require('passport-local').Strategy;
const flash = require('connect-flash');

module.exports = (app) => {


    passport.use(new LocalStrategy(
        async (username, password, done) => {
          if (username == 'roderick@agilecorp.net.pe' && password == 'bk4cybertel') {
            let user = { Name: 'roderick' };
            return done(null, user);
          }
          else {
            return done(null, false, { message: 'Credenciales inválidas' });
          }
        }
      ));
      
      passport.serializeUser((user, done) => {
        const userSessionData = {
          //id: user.Id, 
          //email: user.Email, 
          name: user.Name
        };
        done(null, userSessionData);
      });
      
      passport.deserializeUser((user, done) => {
        done(null, { user });
      });

    app.get('/', async (req, res) => {
        res.render('./app/home', {});
    });

    app.get('/login', async (req, res) => {
        console.log('login');
        res.render('./app/login', { layout: './shared/layout_login' });
    });

    app.post('/login', passport.authenticate('local', {
        successRedirect: '/', // Redirige a '/main' en caso de éxito
        failureRedirect: '/login', // Redirige a '/login' en caso de falla
        failureFlash: true // Activa el uso de flash messages para mostrar un mensaje de error
    }));

    app.get('/logout', (req, res) => {
        req.logout((err) => {
            if (err) {
                return next(err);
            }
            res.redirect('/');
        });
    });
}

