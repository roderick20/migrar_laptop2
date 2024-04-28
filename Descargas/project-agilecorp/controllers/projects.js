//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/projects', async (req, res) => {
        console.log(process.env.DB_SERVER);
        console.log(process.env.DB_NAME);
        const client = new MongoClient(process.env.DB_SERVER);
        try {
            const db = client.db(process.env.DB_NAME);
            const collection = db.collection('projects');
            const projects = await collection.find().toArray();
            res.render('./app/projects/index', { projects: projects });
        }
        catch (ex) {

        }
        finally {
            await client.close();
        }
    });

    app.get('/projects/create', async (req, res) => {
        res.render('./app/projects/create', {});
    });

    app.post('/projects/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('projects');
        req.body.date_begin = new Date(req.body.date_begin + "T00:00:00");
        req.body.date_end = new Date(req.body.date_end + "T00:00:00");
        //req.body.author = 
        //req.body.created = new Date();
        const resultado = await collection.insertOne(req.body);
        res.redirect('/projects');
    });

    app.get('/projects/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('projects');
        const objectId = new ObjectId(req.params.id);
        const project = await collection.findOne({ _id: objectId });
        res.render('./app/projects/update', { project: project });
    });

    app.post('/projects/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('projects');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        req.body.date_begin = new Date(req.body.date_begin + "T00:00:00");
        req.body.date_end = new Date(req.body.date_end + "T00:00:00");
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/projects');
    });

    app.get('/projects/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('projects');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/projects');
    });


}