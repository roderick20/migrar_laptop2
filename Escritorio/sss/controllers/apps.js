//v1.0.0
module.exports = (app) => {
    app.get('/', async (req, res) => {
        res.render('./apps/index', {});
    });    
}