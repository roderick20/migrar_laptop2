//v1.0.0
module.exports = (app) => {

    app.get('/', (req, res) => {
        res.render('auths/index', {
            title: '',
        });
    });

}