//v1.0.0
const modelLogin = require('../models/login');
const passport = require('passport');
const LocalStrategy = require('passport-local').Strategy;

module.exports = (app) => {

    app.get('/login', (req, res) => {
        res.render('auths/login', {
            title: 'Inicio',
            layout: './shared/layout_login.ejs',
            error: ''
        });
    });

    passport.use(new LocalStrategy(
        async (username, password, done) => {
            const loginResult = await modelLogin.login(username, password);
            console.log('Login Result:', loginResult);

            if (loginResult.result) {
                // Comprueba si hay registros en el primer conjunto de resultados (Person)
                if (loginResult.result.recordsets[0].length > 0) {
                    const user = loginResult.result.recordsets[0][0];
                    return done(null, user);
                }
                // Si no hay registros en el primer conjunto, verifica en el segundo conjunto (User)
                else if (loginResult.result.recordsets[1].length > 0) {
                    const user = loginResult.result.recordsets[1][0];
                    return done(null, user);
                }
            }

            return done(null, false, { message: 'Credenciales inválidas' });
        }
    ));

    passport.serializeUser((user, done) => {
        // Serializa el usuario por su ID y tipo de usuario
        done(null, { id: user.Id, name: user.Name, FirstName: user.FirstName, role: user.Role,  userType: user.hasOwnProperty('FirstName') ? 'Person' : 'User' });
    });

    passport.deserializeUser((user, done) => {
        // Reemplaza esto con tu propia lógica para buscar al usuario en la base de datos por su dirección de correo electrónico
        done(null, { user });
    });
    app.post('/login', (req, res, next) => {
        passport.authenticate('local', (err, user, info) => {
            if (err) {
                return next(err);
            }
            if (!user) {
                return res.redirect('/login'); // Redirige a /login si la autenticación falla
            }
            req.logIn(user, (err) => {
                if (err) {
                    return next(err);
                }
                if (user.hasOwnProperty('FirstName')) {
                    // Es un usuario de tipo Person
                    return res.redirect('/persons');
                } else {
                    // Es un usuario de tipo User
                    return res.redirect('/users');
                }
            });
        })(req, res, next);
    });
    
    // app.get('/Logout', (req, res) => {
    //     // Destruir la sesión por completo
    //     // req.session.destroy();
    
    //     // Asignar valores nulos a las propiedades de la sesión
    //     req.session.isLoggedIn = false;
    //     req.session.useName = null;
    //     req.session.userId = null;
    //     req.session.level = null;
        
    //     // Redirigir al usuario a la página de inicio de sesión
    //     res.redirect('/login');
    // });

    app.get('/Logout', (req, res) => {
        // Utiliza req.logout() con una función de devolución de llamada
        req.logout((err) => {
            if (err) {
                return next(err);
            }
    
            // Redirigir al usuario a la página de inicio de sesión
            res.redirect('/login');
        });
    });
    


}