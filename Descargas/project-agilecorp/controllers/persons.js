//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/persons', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const persons = await collection.find().toArray();
        res.render('./app/persons/index', { persons: persons });
    });

    app.get('/persons/create', async (req, res) => {
        res.render('./app/persons/create', {});
    });

    app.post('/persons/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/persons');
    });

    app.get('/persons/read/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const objectId = new ObjectId(req.params.id);
        const person = await collection.findOne({ _id: objectId });
        res.render('./app/persons/read', { person: person });
    });

    app.get('/persons/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const objectId = new ObjectId(req.params.id);
        const person = await collection.findOne({ _id: objectId });
        res.render('./app/persons/update', { person: person });
    });

    app.post('/persons/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/persons');
    });

    app.get('/persons/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('persons');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/persons');
    });

}