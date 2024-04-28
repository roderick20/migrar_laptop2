//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/accounts', async (req, res) => {
        try {
            const client = new MongoClient(process.env.DB_SERVER);
            const db = client.db(process.env.DB_NAME);
            const collection = db.collection('accounts');
            const accounts = await collection.find().toArray();
            res.render('./app/accounts/index', { accounts: accounts });
        }
        catch (ex) {
        }
    });

    app.get('/accounts/create', async (req, res) => {
        res.render('./app/accounts/create', {});
    });

    app.post('/accounts/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('accounts');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/accounts');
    });

    app.get('/accounts/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('accounts');
        const objectId = new ObjectId(req.params.id);
        const account = await collection.findOne({ _id: objectId });
        res.render('./app/accounts/update', { account: account });
    });

    app.post('/accounts/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('accounts');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/accounts');
    });

    app.get('/accounts/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('accounts');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/accounts');
    });

}