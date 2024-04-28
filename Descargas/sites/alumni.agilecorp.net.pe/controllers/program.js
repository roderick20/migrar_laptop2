//v1.0.0
module.exports = (app) => {

    const programs = app.models.program;

app.get('/programs', async (req, res) => {
    try {
        const list = await programs.list();

        res.render('program/index', {
            title: 'Programa',
            program: list.recordset,
            error: ''
        });
    }
    catch (ex) {
        res.render('program/index', {
            title: 'Lista de compañias',
            companies: null,
            error: ex
        });
    }
});

app.get('/programs/create/:error?', async (req, res) => {
    try {
        res.render('program/create', {
            title: 'Agregar Programa',
            error: req.params.error == null ? '' : req.params.error,
        });
    }
    catch (ex) {
        res.render('program/create', {
            title: 'Agregar Programa',
            error: ex
        });
    }
});

app.post('/programs/create', async (req, res) => {
    try {


        await programs.save(req.body);
        res.redirect('/programs/');
    }
    catch (ex) {
        res.redirect('/programs/create/' + ex);
    }
});


app.get('/programs/update/:uniqueid/:error?', async (req, res) => {
    try {
        const list = await programs.select(req.params.uniqueid)

        if (list.recordset.length > 0) {
            res.render('program/update', {
                title: 'Editar Programa',
                program: list.recordset[0],
                error: req.params.error == null ? '' : req.params.error,
            });
        } else {
            res.render('program/update', {
                title: 'Editar Programa',
                user: null,
                error: ' No existe'
            });
        }
    }
    catch (ex) {
        res.render('program/update', {
            title: 'Programa',
            user: null,
            error: ex + " No existe"
        });
    }
});

app.post('/programs/update', async (req, res) => {
    try {
        await programs.update(req.body);
    }
    catch (ex) {
        res.redirect(`/programs/update/${req.body.UniqueId}/${ex}`);
    }
    res.redirect('/programs');
});

app.get('/programs/delete/:uniqueid/:error?', async (req, res) => {
    try {        
        const list = await programs.select(req.params.uniqueid)
        if (list.recordset.length > 0) {
            res.render('program/delete', {
                title: 'Eliminar Programa',
                UniqueId: req.params.uniqueid,
                error: req.params.error == null ? '' : req.params.error

            });
        } else {
            res.render('program/delete', {
                title: 'Editar usuarios',
                UniqueId: null,
                error: ' No existe'
            });
        }
    }
    catch (ex) {
        res.render('program/delete', {
            title: 'Programas',
            UniqueId: req.params.uniqueid,
            error: ex
        });
    }
});

app.post('/programs/delete', async (req, res) => {
    try {
        await programs.deleted(req.body.UniqueId)

        res.redirect('/programs');
    } catch (ex) {
        res.redirect(`/programs/delete/${req.body.UniqueId}/${ex}`);        
    }
});

}