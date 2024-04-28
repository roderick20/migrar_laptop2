const nodemailer = require('nodemailer');
const fs = require('fs');

const transportOptions = {
    host: 'mail5019.site4now.net',
    port: '465',
    auth: { user: 'info@agilex.pe', pass: 'Aladino_09' },
    secureConnection: true,
    tls: {
        secure: false,
        ignoreTLS: true,
        rejectUnauthorized: false
    }
};

 const transporter = nodemailer.createTransport(transportOptions);
 let dataTpl = fs.readFileSync('./public/tpl_email/tpl_register.html', 'utf8');
 dataTpl = dataTpl.replaceAll('%%code%%', 'qqqq');


const sender = {
    name: 'InA - Arequipa Innova',
    address: 'info@agilex.pe',
};

const mailOptions = {
    from: sender,
    to: "roderick20@hotmail.com",
    subject: 'Registro - Corngirmacion de cuenta',
    html: dataTpl,
    attachments: [
        {
            filename: 'imagen1.png',
            path: './public/tpl_email/logo.png',
            cid: 'imagen1@correo.com',
        },
    ]
};

transporter.sendMail(mailOptions, (error, info) => {
console.log(error);
});

console.log("fin");