//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/codes', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const codes = await collection.find().toArray();
        res.render('./app/codes/index', { codes: codes });
    });

    app.get('/codes/create', async (req, res) => {
        res.render('./app/codes/create', {});
    });

    app.post('/codes/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/codes');
    });

    app.get('/codes/read/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const objectId = new ObjectId(req.params.id);
        const code = await collection.findOne({ _id: objectId });
        res.render('./app/codes/read', { code: code });
    });

    app.get('/codes/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const objectId = new ObjectId(req.params.id);
        const code = await collection.findOne({ _id: objectId });
        res.render('./app/codes/update', { code: code });
    });

    app.post('/codes/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/codes');
    });

    app.get('/codes/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('codes');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/codes');
    });

}