//v1.0.0
module.exports = (app) => {

    const flash = require('connect-flash');
    const { MongoClient } = require('mongodb');
    const ObjectId = require('mongodb').ObjectId;

    app.get('/tasks', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const taskCollection = db.collection('tasks');
        const tasks = await taskCollection.aggregate([
            {
                $lookup:
                {
                    from: 'projects',
                    localField: 'project_id',
                    foreignField: '_id',
                    as: 'project'
                }
            }
        ]).toArray();
        res.render('./app/tasks/index', { tasks: tasks });
    });

    app.get('/tasks/calendar', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const taskCollection = db.collection('tasks');
        const tasks = await taskCollection.aggregate([
            {
                $lookup:
                {
                    from: 'projects',
                    localField: 'project_id',
                    foreignField: '_id',
                    as: 'project'
                }
            }
        ]).toArray();
        res.render('./app/tasks/calendar', { tasks: tasks.filter(m => m.status == 'Abierto') });
    });

    app.get('/tasks/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const projectCollection = db.collection('projects');
        const projects = await projectCollection.find().toArray();
        res.render('./app/tasks/create', { projects: projects });
    });

    app.post('/tasks/create', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('tasks');
        req.body.project_id = new ObjectId(req.body.project_id);
        req.body.date_begin = new Date(req.body.date_begin + "T00:00:00");
        req.body.date_end = new Date(req.body.date_end + "T00:00:00");
        const resultado = await collection.insertOne(req.body);
        res.redirect('/tasks');
    });

    app.get('/tasks/update/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);

        const projectCollection = db.collection('projects');
        const projects = await projectCollection.find().toArray();

        const taskCollection = db.collection('tasks');
        const objectId = new ObjectId(req.params.id);
        const task = await taskCollection.findOne({ _id: objectId });
        res.render('./app/tasks/update', { projects: projects, task: task });
    });

    app.post('/tasks/update', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('tasks');
        const objectId = new ObjectId(req.body.id);
        delete req.body.id;
        req.body.project_id = new ObjectId(req.body.project_id);
        req.body.date_begin = new Date(req.body.date_begin + "T00:00:00");
        req.body.date_end = new Date(req.body.date_end + "T00:00:00");
        const resultado = await collection.updateOne(
            { _id: objectId },
            { $set: req.body }
        );
        res.redirect('/tasks');
    });

    app.get('/tasks/delete/:id', async (req, res) => {
        const client = new MongoClient(process.env.DB_SERVER);
        const db = client.db(process.env.DB_NAME);
        const collection = db.collection('tasks');
        const objectId = new ObjectId(req.params.id);
        const documento = await collection.deleteOne({ _id: objectId });
        res.redirect('/tasks/');
    });

}