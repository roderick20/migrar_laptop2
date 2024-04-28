const { Console } = require('console');
const crypto = require('crypto');
const multer = require('multer');
const fs = require('fs');
const { v4: uuidv4 } = require('uuid');
const QRCode = require('qrcode');

//v1.0.0
module.exports = (app) => {

    const personModel = app.models.person;
    const Area = app.models.area;
    const programs = app.models.program;
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

    app.get('/persons/create/:error?', async (req, res) => {
        let data = await personModel.getDataCreate();
        const area = await Area.list();
        const list = await programs.list();
        try {
            res.render('persons/create', {
                title: 'Agregar usuario',
                companies: area.recordsets[0],
                program: list.recordset,
                error: req.params.error == null ? '' : req.params.error,
            });
        }
        catch (ex) {
            res.render('persons/create', {
                title: 'Agregar usuario',

                error: ex
            });
        }
    });

    app.post('/persons/create', upload.fields([{ name: 'inputFile' }]), async (req, res) => {
        const Photo = req.files['inputFile'] ? req.files['inputFile'][0] : null;
        const defaultPhoto = null;
        try {
            var obj = {
                Id_area: req.body.Id_area,
                FirstName: req.body.firstName,
                LastName: req.body.lastName,
                Email: req.body.email,
                Password: req.body.password,
                Role: 1,//req.body.role,
                ProgramId: req.body.ProgramId,

                Photo: Photo ? Photo.filename : defaultPhoto,
                Address: req.body.Address,
                Title: req.body.Title,
                JobTitle: req.body.JobTitle,
                LastAccess: req.body.lastAccess,
                Whatsapp: req.body.Whatsapp,
                WhatsappStatus: req.body.WhatsappStatus,
                Instagram: req.body.Instagram,
                InstagramStatus: req.body.InstagramStatus,
                Facebook: req.body.Facebook,
                FacebookStatus: req.body.FacebookStatus,
                Youtube: req.body.Youtube,
                YoutubeStatus: req.body.YoutubeStatus,
                Telegram: req.body.Telegram,
                TelegramStatus: req.body.TelegramStatus,
                TikTok: req.body.TikTok,
                TikTokStatus: req.body.TikTokStatus,
                Twitter: req.body.Twitter,
                TwitterStatus: req.body.TwitterStatus,
                Snapchat: req.body.Snapchat,
                SnapchatStatus: req.body.SnapchatStatus,
                Spotify: req.body.Spotify,
                SpotifyStatus: req.body.SpotifyStatus,
                SitioWeb: req.body.SitioWeb,
                WebsiteStatus: req.body.WebsiteStatus,
                Linkedin: req.body.Linkedin,
                LinkedinStatus: req.body.LinkedinStatus,
                Paypal: req.body.Paypal,
                PaypalStatus: req.body.PaypalStatus,
                PhoneNumber: req.body.phoneNumber,
                FaceTime: req.body.FaceTime,
                Status: req.body.Status


            };
            await personModel.save(obj);
            res.redirect('/persons/');
        }
        catch (ex) {
            res.redirect('/persons/create/' + ex);
        }
    });

    app.get('/persons/read/:uniqueid', async (req, res) => {
        try {
            const persons = await personModel.select(req.params.uniqueid);

            if (persons.recordset.length > 0) {
                res.render('persons/read', {
                    title: 'Editar Persona',
                    person: persons.recordset[0],
                    error: ''
                });
            } else {
                res.render('persons/read', {
                    title: 'Editar Persona',
                    person: null,
                    error: ' No existe'
                });
            }


        }
        catch (ex) {
            res.render('persons/read', {
                title: 'Personas',
                person: null,
                error: ex + " No existe"
            });
        }
    });

    app.get('/persons/update/:UniqueId', async (req, res) => {
        try {
            const data = await personModel.SelectArea(req.params.UniqueId);
            const persons = await personModel.select(req.params.UniqueId);
            const select = await settings.select(persons.recordset[0].Id);
            const personprogram = await settings.selectProgramPerson(persons.recordset[0].Id)
            console.log(personprogram.recordset)
            const list = await programs.list();
            if (persons && persons.recordset && persons.recordset.length > 0) {
                res.render('persons/update', {
                    title: 'Editar persona',
                    person: persons.recordset[0],
                    companies: data.recordsets[0],
                    settings: select.recordset[0],
                    programperson: personprogram.recordset,
                    program: list.recordset,
                    error: '',
                });
            } else {
                res.render('persons/update', {
                    title: 'Editar persona',
                    person: null,
                    error: 'No existe'
                });
            }
        } catch (ex) {
            res.render('persons/update', {
                title: 'Personas',
                person: null,
                error: ex + " No existe"
            });
        }
    });

    app.post('/persons/update', upload.fields([{ name: 'inputFile' }]), async (req, res) => {
        const Photo = req.files['inputFile'] ? req.files['inputFile'][0] : null;
        try {
            const persons = await personModel.select(req.body.UniqueId);
            if (!persons.error) {
                const personlist = persons.recordset[0];
                if (Photo) {
                    //Eliminar los archivos de la carpeta upload
                    if (personlist.Photo) {
                        fs.unlinkSync(`uploads/${personlist.Photo}`);
                    }
                }
                var obj = {
                    PersonId: req.body.Id,
                    UniqueId: req.body.UniqueId,
                    Id_area: req.body.Id_area,
                    FirstName: req.body.firstName,
                    LastName: req.body.lastName,
                    Email: req.body.email,
                    // Password: req.body.password,
                    ProgramId: req.body.ProgramId,

                    Role: 1,

                    Photo: Photo ? Photo.filename : undefined,
                    Title: req.body.Title,
                    JobTitle: req.body.JobTitle,
                    LastAccess: req.body.lastAccess,
                    Whatsapp: req.body.Whatsapp,
                    WhatsappStatus: req.body.WhatsappStatus,
                    Instagram: req.body.Instagram,
                    InstagramStatus: req.body.InstagramStatus,
                    Facebook: req.body.Facebook,
                    FacebookStatus: req.body.FacebookStatus,
                    Youtube: req.body.Youtube,
                    YoutubeStatus: req.body.YoutubeStatus,
                    Telegram: req.body.Telegram,
                    TelegramStatus: req.body.TelegramStatus,
                    TikTok: req.body.TikTok,
                    TikTokStatus: req.body.TikTokStatus,
                    Twitter: req.body.Twitter,
                    TwitterStatus: req.body.TwitterStatus,
                    Snapchat: req.body.Snapchat,
                    SnapchatStatus: req.body.SnapchatStatus,
                    Spotify: req.body.Spotify,
                    SpotifyStatus: req.body.SpotifyStatus,
                    Website: req.body.Website,
                    WebsiteStatus: req.body.WebsiteStatus,
                    Linkedin: req.body.Linkedin,
                    LinkedinStatus: req.body.LinkedinStatus,
                    Paypal: req.body.Paypal,
                    PaypalStatus: req.body.PaypalStatus,
                    PhoneNumber: req.body.phoneNumber,
                    FaceTime: req.body.FaceTime,
                    Status: req.body.Status

                };
                await personModel.update(obj);
                res.redirect(`/persons/update/${req.body.UniqueId}`);
            } else {

            }
        }
        catch (ex) {
            console.log('hay unerror')
            console.log(ex)
            res.redirect(`/persons/update/${req.body.UniqueId}`);
        }
    });

    app.get('/persons/delete/:UniqueId', async (req, res) => {
        try {
            const persons = await personModel.select(req.params.UniqueId)

            console.log(req.params.error);

            if (persons.recordset.length > 0) {
                console.log(persons);
                res.render('persons/delete', {
                    title: 'Editar persona',
                    UniqueId: req.params.UniqueId,
                    error: ''
                });
            } else {
                res.render('persons/delete', {
                    title: 'Editar persona',
                    UniqueId: null,
                    error: ' No existe'
                });
            }
        }
        catch (ex) {
            res.render('persons/delete', {
                title: 'Personas',
                UniqueId: req.params.uniqueid,
                error: ex
            });
        }
    });

    app.post('/persons/delete', async (req, res) => {
        try {
            //averiguo la ruta del archivo para borrarlo
            const persons = await personModel.select(req.body.UniqueId);
            const personlist = persons.recordset[0];
            const select = await settings.select(persons.recordset[0].Id);
            const selectlist = select.recordset[0];
            //se borra el archivo de la carpeta uploads
            if (personlist.Photo) {
                fs.unlinkSync(`uploads/${personlist.Photo}`);
            }
            if (selectlist.Image) {
                fs.unlinkSync(`uploads/${selectlist.Image}`);
            }
            await personModel.deleted(req.body.UniqueId, persons.recordset[0].Id)

            res.redirect('/persons');
        } catch (ex) {
            res.redirect(`/persons/delete/${req.body.UniqueId}/${ex}`);
        }
    });
    app.get('/persons', async (req, res) => {
        try {
            res.render('persons/index', {
                title: 'Lista de Productos'
                // ,layout: './shared/layout-person.ejs',
            });
        }
        catch (ex) {
        }
    });
    app.get('/personsadmin', async (req, res) => {
        try {
            res.render('persons/index', {
                title: 'Lista de Productos'
            });
        }
        catch (ex) {
        }
    });

    app.post('/persons/findalldt', async (req, res) => {
        try {
            if(req.session.passport.user.role !== 1){
            const personas = await personModel.findAllDT(req.body);
            res.json(personas);
            }else{
            req.body.PersonId = req.session.passport.user.id; //
            const personas = await personModel.findAllDTone(req.body);
            res.json(personas);   
            }
        }
        catch (ex) {
        }
    });

    app.get('/personcard/:UniqueId', async (req, res) => {
        try {
            const persons = await personModel.select(req.params.UniqueId);
            const person = persons.recordset[0];
            const select = await settings.select(persons.recordset[0].Id);
            // Renderizar la vista de la tarjeta de presentación
            res.render('persons/card', {
              title: 'Carta de Presentación',
              layout: './shared/layout_login.ejs',
              person: person,
              settings: select.recordset[0],
            });
        }
        catch (ex) {
        }
    });

    app.get('/personcardqr/:UniqueId', async (req, res) => {
        try {
          const persons = await personModel.select(req.params.UniqueId);
          const person = persons.recordset[0];
          const select = await settings.select(persons.recordset[0].Id);
            //Generamos el QR
            // Generar el código QR y se pueden agregar mas parametros
            const qrCodeURL = `http://localhost:3000/personcard/${req.params.UniqueId}`;
            const qrCodeImage = await QRCode.toDataURL(qrCodeURL);
            //Fin de generacion de QR
          // Renderizar la vista de la tarjeta de presentación
          res.render('persons/cardqr', {
            title: 'Carta de Presentación',
            layout: './shared/layout_login.ejs',
            person: person,
            qrCodeImage: qrCodeImage,
            settings: select.recordset[0],
          });
        } catch (ex) {
          // Manejar cualquier error
          console.error(ex);
          res.status(500).send('Error interno del servidor');
        }
      });

    // Función para convertir una imagen a base64
    function convertImageToBase64(imagePath) {
        // Lee el archivo de la ruta proporcionada
        const imageData = fs.readFileSync(imagePath);

        // Convierte los datos de la imagen a base64
        const base64Data = imageData.toString('base64');

        return base64Data;
    }


    app.get('/downloadVCF/:UniqueId', async (req, res) => {
        try {
            const persons = await personModel.select(req.params.UniqueId);
            const person = persons.recordset[0];
            const imagePath = `./uploads/${person.Photo}`; // Reemplaza con la ruta real de tu imagen
            const base64Image = convertImageToBase64(imagePath);

            // Crear el contenido del archivo VCF
            const vcfContent = `BEGIN:VCARD
VERSION:3.0
N;CHARSET=utf-8:${person.LastName};${person.FirstName};;;;
FN;CHARSET=utf-8:${person.FirstName} ${person.LastName}
TEL;TYPE=Cel:${person.PhoneNumber}
URL;TYPE=Whatsapp:https://api.whatsapp.com/send?phone=${person.PhoneNumber}
TEL;TYPE=Whatsapp:${person.PhoneNumber}
EMAIL;INTERNET;TYPE=Email Work:${person.Email}
URL;TYPE=Popl - Digital Business Card:${person.Website}
ORG;CHARSET=utf-8:
NOTE;CHARSET=utf-8:            
TITLE;CHARSET=utf-8:${person.JobTitle}
PHOTO;ENCODING=b;TYPE=JPEG:${base64Image}
ADR;TYPE=Address;CHARSET=utf-8:;;Peru;;;;
END:VCARD`;

            // Configurar las cabeceras para forzar la descarga
            res.setHeader('Content-Disposition', `attachment; filename=${person.FirstName}_${person.LastName}.vcf`);
            res.setHeader('Content-Type', 'text/vcard');

            // Enviar el archivo como respuesta
            res.send(vcfContent);
        } catch (ex) {
            // Manejar cualquier error
            console.error(ex);
            res.status(500).send('Error interno del servidor');
        }
    });

    app.get('/persons/updatepassword/:UniqueId', async (req, res) => {
        try {
                res.render('persons/updatepassword', {
                    title: 'Actualizar Contraseña',
                    UniqueId: req.params.UniqueId,
                    error: '',
                });

        } catch (ex) {
            res.render('persons/updatepassword', {
                title: 'Actualizar Contraseña',
                person: null,
                error: ex + " No existe"
            });
        }
    });

    app.post('/persons/updatepassword/:UniqueId', async (req, res) => {
        try {
            console.log(req.body)
            await personModel.updatePassword(req.body, req.params.UniqueId)

            res.json({ success: true, message: 'Contraseña actualizada con éxito' });
        } catch (ex) {
            res.status(500).json({ success: false, message: 'Error al actualizar la contraseña' });
        }
    });



}