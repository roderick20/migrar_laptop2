//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/budgets', async (req, res) => {
        try {
            const client = new MongoClient(process.env.DB_SERVER);
            const db = client.db(process.env.DB_NAME);
            const collection = db.collection('budgets');
            const budgets = await collection.find().toArray();
            res.render('./app/budgets/index', { budgets: budgets });
        }
        catch (ex) {
        }
    });

    app.get('/budgets/create', async (req, res) => {
        res.render('./app/budgets/create', {});
    });

    app.post('/budgets/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('budgets');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/budgets');
    });

    app.get('/budgets/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('budgets');
        const objectId = new ObjectId(req.params.id);
        const account = await collection.findOne({ _id: objectId });
        res.render('./app/budgets/update', { account: account });
    });

    app.post('/budgets/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('budgets');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/budgets');
    });

    app.get('/budgets/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('budgets');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/budgets');
    });

}