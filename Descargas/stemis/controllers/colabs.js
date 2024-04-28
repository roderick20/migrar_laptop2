//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const { v4: uuidv4 } = require('uuid');

module.exports = (app) => {
    var colabs = app.models.colabs;
    //List Colabs
    app.get('/admin/colabs', async (req, res) => {
        let colabslist = await colabs.list();
            res.render('colabs/index', {
            title: 'Colaboradores',
            layout: './shared/layout_admin.ejs',
            colabslist: colabslist.result.recordset,
        });
    });    

    app.get('/admin/colabs/create', async (req, res) => {
        let typeColablist = await colabs.typeColab();
            res.render('colabs/create', {
            title: 'Colaboradores',
            layout: './shared/layout_admin.ejs',
            typeColablist: typeColablist.result.recordset,
        });
    }); 
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

    //Post colab
    app.post('/admin/colabs/save', upload.fields([{name: 'Photo' }]), async (req, res) => {
        const { Name, ColabTypeId } = req.body;
        const Photo = req.files['Photo'][0]; 
        try {
        await colabs.postColab(Name, ColabTypeId, Photo.filename);
        res.redirect('/admin/colabs');
        }catch (error) {
            res.status(500).send('Error al enviar el formulario');
        }
    });
    //Delete colab
    app.delete('/admin/colabs/delete/:UniqueId', async (req, res) => {
        const UniqueId = req.params.UniqueId;
        try {
        const colabsResult = await colabs.listbyId(UniqueId);
        if (!colabsResult.error) {
            const colablist = colabsResult.result.recordset[0]; 
            const photoFileName = colablist.Photo;
            //Eliminar los archivos de la carpeta upload
            fs.unlinkSync(`${photoFileName.substring(1)}`);
            await colabs.colabDelete(UniqueId);
            res.sendStatus(204); // Indicando que la eliminación se realizó correctamente
        } else {
            res.status(500).send('Error al obtener los datos del colab');
        }
        } catch (error) {
            res.status(500).send('Error al eliminar al colab');
        }
    });

    //Colabs Data
    app.get('/admin/colabs/edit/:UniqueId', async (req, res) => {
        let colabslist = await colabs.listbyId(req.params.UniqueId)
        let typeColablist = await colabs.typeColab();
        res.render('colabs/update', {
            title: 'Colaboradores',
            layout: './shared/layout_admin.ejs',
            colabslist: colabslist.result.recordset,
            typeColablist: typeColablist.result.recordset,
        });
    });
    //Update Colab
    app.post('/admin/colabs/update/:UniqueId', upload.fields([{name: 'Photo' }]), async(req, res) =>{
        const UniqueId = req.params.UniqueId;
        const { Name, ColabTypeId } = req.body;
        const Photo = req.files['Photo'] ? req.files['Photo'][0]: null; 
        try {
            const colabslistResult = await colabs.listbyId(UniqueId);
            if (!colabslistResult.error) {
                const colablist = colabslistResult.result.recordset[0]; 
                if(Photo){
                     //Eliminar los archivos de la carpeta upload
                    fs.unlinkSync(`${colablist.Photo.substring(1)}`);
                }
                const updateParams = {
                    UniqueId, Name, ColabTypeId, 
                    Photo: Photo ? Photo.filename : null,
                }
                await colabs.colabUpdate(updateParams);
                res.redirect('/admin/colabs');
            } else {
                res.status(500).send('Error al obtener los datos del colaborador');
            }
        } catch (error) {
            res.status(500).send('Error al actualizar los datos del colaborador');
        }
    });
    

}