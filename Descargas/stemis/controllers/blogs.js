//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const { v4: uuidv4 } = require('uuid');
const moment = require('moment');


module.exports = (app) => {
    var blog = app.models.blog;
    var colabs = app.models.colabs;
    app.get('/admin/blogs', async (req, res) => {
        let bloglist = await blog.list();
        res.render('blogs/index', {
            title: 'Blog',
            layout: './shared/layout_admin.ejs',
            bloglist: bloglist.result.recordset,
        });
    });  
    //Form posts
    app.get('/admin/blogs/create', async (req, res) => {
        let colabslist = await colabs.list();
        let listsubject = await blog.listSubject();
        res.render('blogs/create', {
            title: 'Blogs',
            layout: './shared/layout_admin.ejs',
            colabslist: colabslist.result.recordset,
            listsubject: listsubject.result.recordset,
        });
    });

    //image and pdf
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
    //Save post
    app.post('/admin/blogs/save', upload.fields([{name: 'Imagen' }, {name: 'PDFile'}, {name: 'Background'}]), async (req, res) => {
        const {IsPublish, Title, Slug, Tags, PhotoAuthor, SubjectId, PostDate, Judgement, Pleno, Expedient, CardSummary, Body } = req.body;
        const Imagen = req.files['Imagen'][0]; 
        const PDFile = req.files['PDFile'][0]; 
        const Background = req.files['Background'][0];
        const selectedAuthor = req.body['PostAuthor'];
        const [ColabId, PostAuthor] = selectedAuthor.split('|');
        const FormPostDate = moment(PostDate, 'YYYY-MM-DD').format('YYYY-MM-DD HH:mm:ss');

        const publishValue = IsPublish === 'true' ? true : false;

        try {
        await blog.PostBlog(publishValue,Title, Slug, Tags, PhotoAuthor, SubjectId, PostAuthor, FormPostDate, 
            Judgement, Pleno, Expedient, CardSummary, Body, Imagen.filename, PDFile.filename, Background.filename, ColabId);
        res.redirect('/admin/blogs');
        }catch (error) {
            console.error(error); 
            res.status(500).send('Error al enviar el formulario');
        }
    });
    //DeletePost
    app.delete('/admin/blogs/delete/:UniqueId', async (req, res) => {
        const UniqueId = req.params.UniqueId;
        try {
        const blogPostResult = await blog.BlogsId(UniqueId);
        if (!blogPostResult.error) {
            const blogPost = blogPostResult.result.recordset[0]; 
            const imageFileName = blogPost.Imagen; 
            const pdfFileName = blogPost.PDFile;   
            const backgroundFileName = blogPost.Background; 
            //Eliminar los archivos de la carpeta upload
            fs.unlinkSync(`${imageFileName.substring(1)}`);
            fs.unlinkSync(`${pdfFileName.substring(1)}`);
            fs.unlinkSync(`${backgroundFileName.substring(1)}`);

            await blog.blogsDelete(UniqueId);
            res.sendStatus(204); // Indicando que la eliminación se realizó correctamente
        } else {
            res.status(500).send('Error al obtener los datos del post');
        }
        } catch (error) {
            res.status(500).send('Error al eliminar el post');
        }
    });
    //Blog Data
    app.get('/admin/blogs/edit/:UniqueId', async (req, res) => {
        let blogslist = await blog.BlogsId(req.params.UniqueId)
        let listsubject = await blog.listSubject();
        let colabslist = await colabs.list();
        console.log(blogslist.result.recordset)
        res.render('blogs/update', {
            title: 'Blogs',
            layout: './shared/layout_admin.ejs',
            blogslist: blogslist.result.recordset,
            listsubject: listsubject.result.recordset,
            colabslist: colabslist.result.recordset,
        });
    });
    //Update Blog
    app.post('/admin/blogs/update/:UniqueId', upload.fields([{name: 'Imagen' }, {name: 'PDFile'}, {name: 'Background'}]), async(req, res) =>{
        const UniqueId = req.params.UniqueId;
        const {Title, Slug, Tags, PhotoAuthor, SubjectId, PostDate, Judgement, Pleno, Expedient, CardSummary, Body } = req.body;
        const Imagen = req.files['Imagen'] ? req.files['Imagen'][0]: null; 
        const PDFile = req.files['PDFile'] ? req.files['PDFile'][0]: null; 
        const Background = req.files['Background'] ? req.files['Background'][0]: null; 
        const selectedAuthor = req.body['PostAuthor'];
        let IsPublish = req.body.IsPublish ; 
        const [ColabId, PostAuthor] = selectedAuthor.split('|');
        const FormPostDate = moment(PostDate, 'YYYY-MM-DD').format('YYYY-MM-DD HH:mm:ss');
        try {
            const blogPostResult = await blog.BlogsId(UniqueId);

            if (!blogPostResult.error) {
                const blogPost = blogPostResult.result.recordset[0]; 
                
                if(Imagen){
                    fs.unlinkSync(`${blogPost.Imagen.substring(1)}`);
                }
                if(PDFile){
                fs.unlinkSync(`${blogPost.PDFile.substring(1)}`);
                }
                if(Background){
                fs.unlinkSync(`${blogPost.Background.substring(1)}`);
                }

                const updateParams = {
                    UniqueId, IsPublish,Title, Slug, Tags, 
                    PhotoAuthor, SubjectId, PostAuthor, FormPostDate, 
                    Judgement, Pleno, Expedient, CardSummary, Body, 
                    Imagen:Imagen ? Imagen.filename : null, 
                    PDFile: PDFile ? PDFile.filename : null,
                    Background: Background ? Background.filename : null, 
                    ColabId

                }
                await blog.blogsUpdate(
                    updateParams
                );
                res.redirect('/admin/blogs');
            } else {
                res.status(500).send('Error al obtener los datos del blog');
            }
        } catch (error) {
            res.status(500).send('Error al actualizar el blog');
        }
    });
    
}