//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/blogs', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const blogs = await collection.find().toArray();
        res.render('./app/blogs/index', { blogs: blogs });
    });

    app.get('/blogs/create', async (req, res) => {
        res.render('./app/blogs/create', {});
    });

    app.post('/blogs/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const resultado = await collection.insertOne(req.body);
        res.redirect('/blogs');
    });

    app.get('/blogs/read/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const objectId = new ObjectId(req.params.id);
        const blog = await collection.findOne({ _id: objectId });
        res.render('./app/blogs/read', { blog: blog });
    });

    app.get('/blogs/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const objectId = new ObjectId(req.params.id);
        const blog = await collection.findOne({ _id: objectId });
        res.render('./app/blogs/update', { blog: blog });
    });

    app.post('/blogs/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/blogs');
    });

    app.get('/blogs/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('blogs');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/blogs');
    });

}