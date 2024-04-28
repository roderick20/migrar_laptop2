//v1.0.0
module.exports = (app) => {

    app.get('/public/:code', (req, res) => {
        res.render('public/tarjeta', {
            title: 'Tarjeta',
            layout: './shared/layout_login.ejs',
            error: req.params.error == null ? '' : req.params.error,
        });
    });
    app.get('/personeria', async (req, res) => {
        try {
            res.render('persons/index', {
                title: 'Lista de Productos'
            });
        }
        catch (ex) {		
        }
    });
    
    app.post('/personeria/findalldt', async (req, res) => {
        try {
            const productos = await producto.findAllDT(req.body);
            res.json(productos);
        }
        catch (ex) {		
        }
    });
    

}