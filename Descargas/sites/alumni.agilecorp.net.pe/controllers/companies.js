//v1.0.0
module.exports = (app) => {

    const companyModel = app.models.company;

app.get('/companies', async (req, res) => {
    try {
        const companies = await companyModel.list();

        res.render('companies/index', {
            title: 'companies',
            companies: companies.recordset,
            error: ''
        });
    }
    catch (ex) {
        res.render('companies/index', {
            title: 'Lista de compañias',
            companies: null,
            error: ex
        });
    }
});

app.get('/companies/create/:error?', async (req, res) => {
    try {
        res.render('companies/create', {
            title: 'Agregar compañia',
            error: req.params.error == null ? '' : req.params.error,
        });
    }
    catch (ex) {
        res.render('companies/create', {
            title: 'Agregar compañia',
            error: ex
        });
    }
});

app.post('/companies/create', async (req, res) => {
    try {


        await companyModel.save(req.body);
        res.redirect('/companies/');
    }
    catch (ex) {
        res.redirect('/companies/create/' + ex);
    }
});

app.get('/companies/read/:uniqueid', async (req, res) => {
    try {
        const companies = await company.select(req.params.uniqueid);

        if (companies.recordset.length > 0) {
            res.render('companies/read', {
                title: 'Editar usuarios',
                company: companies.recordset[0],
                error: ''
            });
        } else {
            res.render('companies/read', {
                title: 'Editar usuarios',
                company: null,
                error: ' No existe'
            });
        }
        
    }
    catch (ex) {
        res.render('companies/read', {
            title: 'Compañias',
            company: null,
            error: ex + " No existe"
        });
    }
});

app.get('/companies/update/:uniqueid/:error?', async (req, res) => {
    try {
        const companies = await companyModel.select(req.params.uniqueid)

        if (companies.recordset.length > 0) {
            res.render('companies/update', {
                title: 'Editar compañia',
                company: companies.recordset[0],
                error: req.params.error == null ? '' : req.params.error,
            });
        } else {
            res.render('companies/update', {
                title: 'Editar compañia',
                user: null,
                error: ' No existe'
            });
        }
    }
    catch (ex) {
        res.render('companies/update', {
            title: 'Compañias',
            user: null,
            error: ex + " No existe"
        });
    }
});

app.post('/companies/update', async (req, res) => {
    try {
        
        
   

        await companyModel.update(req.body);
    }
    catch (ex) {
        res.redirect(`/companies/update/${req.body.UniqueId}/${ex}`);
    }
    res.redirect('/companies');
});

app.get('/companies/delete/:uniqueid/:error?', async (req, res) => {
    try {        
        const companies = await companyModel.select(req.params.uniqueid)
       
        console.log(req.params.error);

        if (companies.recordset.length > 0) {
            console.log(companies.recordset);
            res.render('companies/delete', {
                title: 'Editar usuarios',
                UniqueId: req.params.uniqueid,
                error: req.params.error == null ? '' : req.params.error

            });
        } else {
            res.render('companies/delete', {
                title: 'Editar usuarios',
                UniqueId: null,
                error: ' No existe'
            });
        }
    }
    catch (ex) {
        res.render('companies/delete', {
            title: 'Comañias',
            UniqueId: req.params.uniqueid,
            error: ex
        });
    }
});

app.post('/companies/delete', async (req, res) => {
    try {
        await companyModel.deleted(req.body.UniqueId)

        res.redirect('/companies');
    } catch (ex) {
        res.redirect(`/companies/delete/${req.body.UniqueId}/${ex}`);        
    }
});

}