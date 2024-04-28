//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const { v4: uuidv4 } = require('uuid');

module.exports = (app) => {

    var normativity = app.models.normatividad;
    //List Norms Crud
    app.get('/admin/normativity', async (req, res) => {
        let listnorm = await normativity.list();
        res.render('normativity/index', {
            title: 'Normatividad',
            layout: './shared/layout_admin.ejs',
            listnorm: listnorm.result.recordset,
        });
    });
    //form norm
    app.get('/admin/normativity/create', async (req, res) => {
        res.render('normativity/create', {
            title: 'Normativity',
            layout: './shared/layout_admin.ejs',
        });
    });
    //Image and PDF
    const storage = multer.diskStorage({
        destination: (req, file, cb) => {
          cb(null, 'uploads/'); // Directorio donde se guardarán los archivos
        },
        filename: (req, file, cb) => {
            const ext = file.originalname.split('.').pop();
            let filename = file.originalname; // Mantener el nombre original por defecto
    
            // Si es una imagen, generamos un nuevo nombre con UUID
            if (file.mimetype.startsWith('image/')) {
                filename = `${uuidv4()}.${ext}`;
            }
    
            cb(null, filename);
        },
      });
      const upload = multer({ storage: storage });
    //Save norm
    app.post('/admin/normativity/save', upload.fields([{name: 'CardPhoto' }, {name: 'Photo'}, {name: 'PDFile'}]), async (req, res) => {
        const { IsPublish, Title, Slug, Tags, Summary, Body } = req.body;
        const CardPhoto = req.files['CardPhoto'][0]; 
        const Photo = req.files['Photo'][0]; 
        const PDFile = req.files['PDFile'][0];

        const publishValue = IsPublish === 'true' ? true : false;
        try {
            await normativity.normCreate(publishValue, Title, Slug, Tags, Summary, Body, PDFile.filename, Photo.filename, CardPhoto.filename);
             res.redirect('/admin/normativity');
        }catch (error) {
            res.status(500).send('Error al crear la norma');
        }
    });
    //DeleteNorm
    app.delete('/admin/normativity/delete/:UniqueId', async (req, res) => {
        const UniqueId = req.params.UniqueId;
        try {
        const NormResult = await normativity.normsbyId(UniqueId);
        if (!NormResult.error) {
            const norms = NormResult.result.recordset[0]; 
            const photoFileName = norms.Photo;
            const pdfFileName = norms.PDFile;
            const cardphotoFileName = norms.CardPhoto;
            //Eliminar los archivos de la carpeta upload
            fs.unlinkSync(`${photoFileName.substring(1)}`);
            fs.unlinkSync(`${pdfFileName.substring(1)}`);
            fs.unlinkSync(`${cardphotoFileName.substring(1)}`);

            await normativity.normDelete(UniqueId);
            res.sendStatus(204); // Indicando que la eliminación se realizó correctamente
        } else {
            res.status(500).send('Error al obtener los datos del blog');
        }
        } catch (error) {
            res.status(500).send('Error al eliminar el usuario');
        }
    });
    //NormData
    app.get('/admin/normativity/edit/:UniqueId', async(req, res) =>{
        let normlist = await normativity.normsbyId(req.params.UniqueId)
        console.log(normlist.result.recordset)
        res.render('normativity/update', {
            title: 'Normativity',
            layout: './shared/layout_admin.ejs',
            normlist: normlist.result.recordset
        });
    });
    //Norm update
    app.post('/admin/normativity/update/:UniqueId', upload.fields([{name: 'CardPhoto' }, {name: 'Photo'}, {name: 'PDFile'}]), async (req, res) => {
        const UniqueId = req.params.UniqueId;
        const { IsPublish, Title, Slug, Tags, Summary, Body } = req.body;
        const CardPhoto = req.files['CardPhoto'] ? req.files['CardPhoto'][0] : null; 
        const Photo = req.files['Photo'] ? req.files['Photo'][0] : null; 
        const PDFile = req.files['PDFile'] ? req.files['PDFile'][0] : null;
        try {
            const NormResult = await normativity.normsbyId(UniqueId);
            if (!NormResult.error) {
                const norms = NormResult.result.recordset[0]; 

                if (Photo) {
                    fs.unlinkSync(`${norms.Photo.substring(1)}`);
                }
                if (PDFile) {
                    fs.unlinkSync(`${norms.PDFile.substring(1)}`);
                }
                if (CardPhoto) {
                    fs.unlinkSync(`${norms.CardPhoto.substring(1)}`);
                }

                const updateParams = {
                    UniqueId,
                    IsPublish,
                    Title,
                    Slug,
                    Tags,
                    Summary,
                    Body,
                    PDFile: PDFile ? PDFile.filename : null,
                    Photo: Photo ? Photo.filename : null,
                    CardPhoto: CardPhoto ? CardPhoto.filename : null
                };

                await normativity.normUpdate(
                    updateParams
                );

                res.redirect('/admin/normativity');
            } else {
                res.status(500).send('Error al obtener los datos de la norma');
            }
        } catch (error) {
            res.status(500).send('Error al actualizar la norma');
        }
    });

    



};