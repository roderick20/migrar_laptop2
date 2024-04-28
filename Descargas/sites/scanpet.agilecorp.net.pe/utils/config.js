const db = {
    server: process.env.DB_Server,
    database: process.env.DB_Name,
    user: process.env.DB_User,
    password: process.env.DB_Password,
    options: {
        encrypt: false,
        trustServerCertificate: true,
    }
};

module.exports = {
    db
};