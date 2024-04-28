//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/diagrams', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const diagrams = await collection.find().toArray();
        res.render('./app/diagrams/index', { diagrams: diagrams });
    });

    app.get('/diagrams/create', async (req, res) => {
        res.render('./app/diagrams/create', {});
    });

    app.post('/diagrams/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/diagrams');
    });

    app.get('/diagrams/read/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const objectId = new ObjectId(req.params.id);
        const diagram = await collection.findOne({ _id: objectId });
        res.render('./app/diagrams/read', { diagram: diagram });
    });

    app.get('/diagrams/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const objectId = new ObjectId(req.params.id);
        const diagram = await collection.findOne({ _id: objectId });
        res.render('./app/diagrams/update', { diagram: diagram });
    });

    app.post('/diagrams/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/diagrams');
    });

    app.get('/diagrams/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('diagrams');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/diagrams');
    });

}