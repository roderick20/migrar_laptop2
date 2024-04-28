//v1.0.0
const crypto = require('crypto');
const fs = require('fs');
const multer = require('multer');
const uuid = require('uuid');

module.exports = (app) => {
    var blog = app.models.blog;
    var normatividad = app.models.normatividad;

    app.get('/', async (req, res) => {
        res.render('public/index', {
            title: 'Inicio',
        });
    });
    app.get('/Simulador', async (req, res) => {
        res.render('public/simulator', {
            title: 'Simulador',
        });
    });
    app.get('/Cursos', async (req, res) => {
        res.render('public/cursos', {
            title: 'Cursos',
        });
    });
    app.get('/Concursos', async (req, res) => {
        res.render('public/concursos', {
            title: 'Concursos',
        });
    });
    //List Blog
    app.get('/Blog', async (req, res) => {
        let bloglist = await blog.list();
        res.render('public/blog', {
            title: 'Blog',
            bloglist: bloglist.result.recordset,
        });
    });
    //Tags Blogs
    app.get('/Blogt', async (req, res) => {
        let tag = req.query.tag;
        let bloglist = await blog.listByTag(tag);
        
        res.render('public/blog', {
            title: 'Blog',
            bloglist: bloglist.result.recordset,
        });
    });
    //Search Blog
    app.get('/Blogs', async (req, res) => {
        let palabraClave = req.query.palabraClave;
        //verificacion
        if (!palabraClave) {
            return res.redirect('/Blog');
        }
        let bloglist = await blog.search(palabraClave);
        res.render('public/blog', {
            title: 'Blog',
            bloglist: bloglist.result.recordset,
        });
    });
    //Posts_blog
    app.get('/Post/:Slug', async (req, res) => {
        let bloglist = await blog.PostId(req.params.Slug)

        res.render('public/post', {
            title: 'Post',
            bloglist: bloglist.result.recordset,
        });
    });
    //List Normatividad
    app.get('/Normatividad', async (req, res) => {
        let normlist = await normatividad.list();
    
        res.render('public/normatividad', {
            title: 'Normatividad',
            normlist: normlist.result.recordset,
        
        });
    });
    //Normas posts
    app.get('/Norma/:Slug', async (req, res) => {
        let normlist = await normatividad.NormId(req.params.Slug);
        res.render('public/norma', {
            title: 'Normatividad',
            normlist: normlist.result.recordset,
        
        });
    });
    //Search Normativas
    app.get('/Norms', async (req, res) => {
        let palabraClave = req.query.palabraClave;
        //verificacion
        if (!palabraClave) {
            return res.redirect('/Normatividad');
        }
        let normlist = await normatividad.search(palabraClave);
        res.render('public/normatividad', {
            title: 'Normatividad',
            normlist: normlist.result.recordset,
        });
    });
    app.get('/Contacto', async (req, res) => {
        res.render('public/contacto', {
            title: 'Contacto',
        });
    });
    // app.get('/', async (req, res) => {
    //     res.render('home/login', {
    //         title: 'Inicio',
    //         layout: './shared/layout_login.ejs',
    //         error: ''
    //     });
    // });



    // app.post('/', async (req, res) => {
    //     console.log(req.body)
    //     let login = await user.login(req.body);

    //     if (login.result.recordsets[0].length > 0) {
    //         user = login.result.recordset[0];
    //         req.session.user = {
    //             userName: user.Nombre,
    //             userId: user.UsuarioID
    //         };
    //         res.redirect('/main');
    //     }
    //     else {
    //         res.redirect('/');
    //     }
    // });

    app.get('/logout', (req, res) => {
        req.session.destroy();
        res.redirect('/');
    });
}