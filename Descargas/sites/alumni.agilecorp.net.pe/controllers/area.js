const area = require("../models/area");

//v1.0.0
module.exports = (app) => {

    const Area = app.models.area;

    app.get('/area', async (req, res) => {
        try {
            const area = await Area.list();

            res.render('Area/index', {
                title: 'Lista de Areas',
                area: area.recordset,
                error: ''
            });
        }
        catch (ex) {
            res.render('users/index', {
                title: 'Lista de usuarios',
                users: null,
                error: ex
            });
        }
    });

    app.get('/area/create/:error?', async (req, res) => {
        try {
            res.render('area/create', {
                title: 'Agregar usuario',
                error: req.params.error == null ? '' : req.params.error,
            });
        }
        catch (ex) {
            res.render('area/create', {
                title: 'Agregar usuario',
                error: ex
            });
        }
    });

    app.post('/area/create', async (req, res) => {
        try {
            var obj = {
                // Id_area: req.body.Id_area,
                area: req.body.area,
            };
            await Area.save(obj);
            res.redirect('/area/');
        }
        catch (ex) {
            res.redirect('/area/create/' + ex);
        }
    });

    app.get('/area/read/:uniqueid', async (req, res) => {
        try {
            const area = await area.select(req.params.uniqueid);

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
    app.get('/recetas/createform/', async (req, res, next) => {
        try {
            const grupos = await receta.listproducto();
            const tabla = await receta.listtablereceta();
            //console.log(grupos.result.recordset);
            res.render('recetas/create', {
                title: 'Agregar Producto',
                grupos: grupos.result.recordsets[0],
                datos: tabla.result.recordsets
            });
        }
        catch (ex) {
            const error = new Error(ex);
            error.status = 500;
            next(error);
        }
    });
    app.get('/area/update/:Id_area', async (req, res) => {
        try {
            const id ={
                ID: req.params.Id_area
            }
            const area = await Area.select(req.params.Id_area)
            const user = await Area.listusuario();
            const arealist = await Area.listarea();
            console.log('DATOS:' + req.params.Id_area);
            console.log(user.result.recordsets);
            if (area.recordset.length > 0) {
                res.render('Area/update', {
                    title: 'Editar usuarios',
                    area: area.recordset[0],
                    usuarios: user.result.recordsets[0],
                    listareas: arealist.result.recordsets[0],

                    error: req.params.error == null ? '' : req.params.error,
                });
            } else {
                res.render('Area/update', {
                    title: 'Editar usuarios',
                    user: null,
                    error: ' No existe'
                });
            }
        }
        catch (ex) {
            res.render('Area/update', {
                title: 'Usuarios',
                user: null,
                error: ex + " No existe"
            });
        }
    });

    app.post('/area/update', async (req, res) => {
        try {
            console.log(req.body);
            await Area.update(req.body);
        }
        catch (ex) {
            // res.redirect(`/users/update/${req.body.Id_area}/${ex}`);
        }
        res.redirect('/area');
    });

    app.get('/area/delete/:UniqueId/:error?', async (req, res) => {
        try {
            const users = await Area.select(req.params.UniqueId); // Change to `users`
    
            console.log(req.params.error);
    
            if (users.recordset.length > 0) { // Change to `users`
                console.log(users);
                res.render('area/delete', {
                    title: 'Editar usuario',
                    UniqueId: req.params.UniqueId,
                    error: req.params.error == null ? '' : req.params.error,
                });
            } else {
                res.render('area/delete', {
                    title: 'Editar usuario',
                    UniqueId: null,
                    error: ' No existe',
                });
            }
        } catch (ex) {
            res.render('area/delete', {
                title: 'Tareas',
                UniqueId: req.params.UniqueId,
                error: ex,
            });
        }
    });

    app.post('/area/delete', async (req, res) => {
        try {
            await Area.deleted(req.body.Id_area)

            res.redirect('/area');
        } catch (ex) {
            res.redirect(`/area/delete/${req.body.UniqueId}/${ex}`);
        }
    });

}