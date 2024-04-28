//v1.0.0

module.exports = (app) => {

    const user = app.models.user;

    app.get('/users/create/:error?', async (req, res) => {
        try {
            res.render('users/create', {
                title: 'Agregar usuario',
                error: req.params.error == null ? '' : req.params.error,
            });
        }
        catch (ex) {
            res.render('users/create', {
                title: 'Agregar usuario',
                error: ex
            });
        }
    });

    app.post('/users/create', async (req, res) => {
        try {
            var obj = {
                Name: req.body.name,
                LastName: req.body.lastName,
                Email: req.body.email,
                Password: req.body.password,
                Enabled: req.body.enabled,
                Role: req.body.role
            };
            await user.save(obj);
            res.redirect('/users/');
        }
        catch (ex) {
            res.redirect('/users/create/' + ex);
        }
    });

    app.get('/users/read/:uniqueid', async (req, res) => {
        try {
            const users = await user.select(req.params.uniqueid);

            if (users.recordset.length > 0) {
                res.render('users/read', {
                    title: 'Ver usuarios',
                    user: users.recordset[0],
                    error: ''
                });
            } else {
                res.render('users/read', {
                    title: 'Ver usuarios',
                    user: null,
                    error: ' No existe'
                });
            }

        }
        catch (ex) {
            res.render('users/read', {
                title: 'Usuarios',
                user: null,
                error: ex + " No existe"
            });
        }
    });

    app.get('/users/update/:uniqueid/:error?', async (req, res) => {
        try {
            const users = await user.select(req.params.uniqueid)

            if (users.recordset.length > 0) {
                res.render('users/update', {
                    title: 'Editar usuarios',
                    user: users.recordset[0],
                    error: req.params.error == null ? '' : req.params.error
                });
            } else {
                res.render('users/update', {
                    title: 'Editar usuarios',
                    user: null,
                    error: ' No existe'
                });
            }
        }
        catch (ex) {
            res.render('users/update', {
                title: 'Usuarios',
                user: null,
                error: ex + " No existe"
            });
        }
    });

    app.post('/users/update', async (req, res) => {
        try {
            console.log(req.body);
            await user.update(req.body);
        }
        catch (ex) {
            res.redirect(`/users/update/${req.body.UniqueId}/${ex}`);
        }
        res.redirect('/users');
    });

    app.get('/users/delete/:uniqueid/:error?', async (req, res) => {
        try {
            const users = await user.select(req.params.uniqueid)

            console.log(req.params.error);

            if (users.recordset.length > 0) {
                console.log(users);
                res.render('users/delete', {
                    title: 'Editar usuario',
                    UniqueId: req.params.uniqueid,
                    error: req.params.error == null ? '' : req.params.error
                });
            } else {
                res.render('users/delete', {
                    title: 'Editar usuario',
                    UniqueId: null,
                    error: ' No existe'
                });
            }
        }
        catch (ex) {
            res.render('users/delete', {
                title: 'Tareas',
                UniqueId: req.params.uniqueid,
                error: ex
            });
        }
    });

    app.post('/users/delete', async (req, res) => {
        try {
            await user.deleted(req.body.UniqueId)

            res.redirect('/users');
        } catch (ex) {
            res.redirect(`/users/delete/${req.body.UniqueId}/${ex}`);
        }
    });
    app.get('/users', async (req, res) => {
        try {
            res.render('users/index', {
                title: 'Lista de usuarios',
            });
        }
        catch (ex) {
        }
    });
    app.post('/users/findalldt', async (req, res) => {
        try {
            const productos = await user.findAllDT(req.body);
            console.log(productos)
            res.json(productos);
        }
        catch (ex) {
        }
    });

    app.get('/users/updatepassword/:UniqueId', async (req, res) => {
        try {
                res.render('users/updatepassword', {
                    title: 'Actualizar Contraseña',
                    UniqueId: req.params.UniqueId,
                    error: '',
                });

        } catch (ex) {
            res.render('users/updatepassword', {
                title: 'Actualizar Contraseña',
                person: null,
                error: ex + " No existe"
            });
        }
    });

    app.post('/users/updatepassword/:UniqueId', async (req, res) => {
        try {
            console.log(req.body)
            await user.updatePassword(req.body, req.params.UniqueId)

            res.json({ success: true, message: 'Contraseña actualizada con éxito' });
        } catch (ex) {
            res.status(500).json({ success: false, message: 'Error al actualizar la contraseña' });
        }
    });

}