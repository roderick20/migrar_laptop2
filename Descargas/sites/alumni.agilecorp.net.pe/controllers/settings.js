//v1.0.0
const { Console } = require('console');
const crypto = require('crypto');
const multer = require('multer');
const fs = require('fs');
const { v4: uuidv4 } = require('uuid');
const QRCode = require('qrcode');

module.exports = (app) => {

    const settings = app.models.settings;

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

    app.get('/settings/create/:PersonId/:UniqueIdPerson', async (req, res) => {
        const select = await settings.select(req.params.PersonId);
        try {
            res.render('settings/create', {
                title: 'Fondo',
                settings: select.recordset[0],
                UniqueId: req.params.UniqueIdPerson,
                PersonId: req.params.PersonId

            });
        }
        catch (ex) {
            res.render('settings/create', {
                title: 'Fondo',
            });
        }
    });

    app.post('/settings/update/:UniqueId/:UniqueIdPerson', upload.fields([{ name: 'inputFile' }]), async (req, res) => {
        const Image = req.files['inputFile'] ? req.files['inputFile'][0] : null;
        try {

            const select = await settings.select(req.body.PersonId);
                const list = select.recordset[0];
                if (Image && list.Image) {
                    // Eliminar los archivos de la carpeta upload
                    fs.unlinkSync(`uploads/${list.Image}`);
                }
            
            var obj = {
                PersonId: req.body.PersonId,
                Image: Image ? Image.filename : null,
                UniqueId: req.params.UniqueId
            };
            await settings.update(obj);
            res.redirect(`/persons/update/${req.params.UniqueIdPerson}`);

        }
        catch (ex) {
            console.error(ex);
            res.status(500).send('Internal Server Error'); 
        }
    });

}