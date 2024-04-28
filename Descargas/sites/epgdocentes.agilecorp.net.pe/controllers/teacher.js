// const { Console } = require('console');
// const crypto = require('crypto');

// const { v4: uuidv4 } = require('uuid');
// const QRCode = require('qrcode');
const multer = require('multer');
const path = require('path');
const fs = require('fs');

const storagefoto = multer.diskStorage({
    destination: (req, file, cb) => {
        // Ruta donde se almacenarán las fotos
        const uploadDir = './uploads/foto';

        // Crea el directorio de carga si no existe
        if (!fs.existsSync(uploadDir)) {
            fs.mkdirSync(uploadDir);
        }

        cb(null, uploadDir);
    },
    filename: (req, file, cb) => {
        // Genera un nombre único para el archivo
        const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1E9);
        const extension = path.extname(file.originalname);
        cb(null, file.fieldname + '-' + uniqueSuffix + extension);
    }
});

const storagecv = multer.diskStorage({
    destination: (req, file, cb) => {
        // Ruta donde se almacenarán las fotos
        const uploadDir = './uploads/cv';

        // Crea el directorio de carga si no existe
        if (!fs.existsSync(uploadDir)) {
            fs.mkdirSync(uploadDir);
        }

        cb(null, uploadDir);
    },
    filename: (req, file, cb) => {
        // Genera un nombre único para el archivo
        const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1E9);
        const extension = path.extname(file.originalname);
        console.log(uniqueSuffix + extension);
        cb(null, file.fieldname + '-' + uniqueSuffix + extension);
    }
});

const uploadfoto = multer({ storage: storagefoto });
const uploadcv = multer({ storage: storagecv });
//v1.0.0
module.exports = (app) => {
    var teachers = app.models.teacher;
    var paisesModel = app.models.paises;
    var universidadModal = app.models.universidades;
    var gradoModal = app.models.grado;
    var planModal = app.models.plan_estudio;
    var teacherplanModal = app.models.teacher_plan;

    function removeDuplicates(array) {
        return array.filter((value, index, self) => {
            return self.indexOf(value) === index;
        });
    }


    app.get('/teacher', async (req, res) => {
        console.log('adfsngsaqui va el user iddfgui ', req.session.passport.user.id)
        try {
            const list = await teachers.findAllId(req.session.passport.user.id)
            const grados = await gradoModal.listByTeacherId(req.session.passport.user.id);
            const teacherplan = await teacherplanModal.findByTeacherId(req.session.passport.user.id);

            let data = [];

            teacherplan.result.recordset.forEach(obj => {
                data.push(obj.PlanEstudio);
                data.push(obj.Grado);
                data.push(obj.Modalidad);
                data.push(obj.Programa);
                data.push(obj.Asignatura);
            });




            res.render('teachers/index', {
                title: 'Formulario',
                teacher: list.result.recordset[0],
                grados: grados.recordset,
                teacherplan: Array.from(new Set(data))
            });
        }
        catch (ex) {
            console.log(ex);
        }
    });

    app.post('/teacher/subirfoto', uploadfoto.single('imagen'), async (req, res) => {
        if (req.file) {

            const list = await teachers.updateFoto(req.session.passport.user.id, req.file.filename);
            res.json({ mensaje: 'Foto subida con éxito', archivo: req.file });
        } else {

            res.status(500).json({ error: 'Error al subir la foto' });
        }
    });

    app.post('/teacher/subircv', uploadcv.single('filecv'), async (req, res) => {
        if (req.file) {

            console.log(req.session.passport.user.id);
            console.log(req.file.filename);

            await teachers.updatecv(req.session.passport.user.id, req.file.filename);
            res.json({ mensaje: 'Foto subida con éxito', archivo: req.file });
        } else {

            res.status(500).json({ error: 'Error al subir la foto' });
        }
    });

    app.post('/teacher/universidad', async (req, res) => {
        let paises = await universidadModal.listByPaises(req.body.pais);
        res.json(paises.recordset);
    });

    app.post('/teacher/agregargrado', async (req, res) => {
        await gradoModal.create(req.body, req.session.passport.user.id);
        const grados = await gradoModal.listByTeacherId(req.session.passport.user.id);
        res.json(grados.recordset);
    });

    app.post('/teacher/eliminargrado', async (req, res) => {
        await gradoModal.delete(req.body);
        const grados = await gradoModal.listByTeacherId(req.session.passport.user.id);
        res.json(grados.recordset);
    });

    app.post('/teacher/plangrado', async (req, res) => {
        const result = await planModal.findGrado(req.body);
        res.json(result.recordset);
    });

    app.post('/teacher/planmodalidad', async (req, res) => {
        const result = await planModal.findModalidad(req.body);
        res.json(result.recordset);
    });

    app.post('/teacher/planprograma', async (req, res) => {
        const result = await planModal.findPrograma(req.body);
        res.json(result.recordset);
    });

    app.post('/teacher/planasignatura', async (req, res) => {
        const result = await planModal.findAsignatura(req.body);
        res.json(result.recordset);
    });

    app.post('/teacher/agregarplan', async (req, res) => {
        try {
            await teacherplanModal.create(req.body, req.session.passport.user.id);
            const result = await teacherplanModal.findByTeacherId(req.session.passport.user.id);
            console.log(result);
            res.json(result.result.recordset);
        }
        catch (ex) {
            console.log(ex);
        }
    });
    app.post('/teacher/eliminarplan', async (req, res) => {
        try {
            await teacherplanModal.delete(req.body.PlanEstudioId);
            const result = await teacherplanModal.findByTeacherId(req.session.passport.user.id);
            console.log(result);
            res.json(result.result.recordset);
        }
        catch (ex) {
            console.log(ex);
        }
    });

    app.get('/teacher/update', async (req, res) => {
        try {
            const list = await teachers.findAllId(req.session.passport.user.id);
            const paises = await paisesModel.list();
            const grados = await gradoModal.listByTeacherId(req.session.passport.user.id);
            const plan = await planModal.findPlanEstudio();
            const teacherplan = await teacherplanModal.findByTeacherId(req.session.passport.user.id);
            console.log(teacherplan.result.recordset);
            res.render('teachers/update', {
                title: 'Formulario',
                list: list.result.recordset,
                paises: paises.recordset,
                grados: grados.recordset,
                plan: plan.recordset,
                teacherplan: teacherplan.result.recordset,
            });
        }
        catch (ex) {
            console.log(ex);
        }
    });

    app.post('/teacher/update', async (req, res) => {
        try {

            console.log(req.body);


            // uploadcv.single('filecv')  (req, res, async (err) => {
            //     if(req.file != undefined){
            //     await teachers.updatecv(req.session.passport.user.id, req.file.filename);
            //     }
            // });


            await teachers.update(req.session.passport.user.id, req.body);
            res.redirect(`/teacher`);
        } catch (error) {

        }
    });





}