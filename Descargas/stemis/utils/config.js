const db = {
    server: process.env.SQL_SERVER,
    database: process.env.SQL_DATABASE,
    user: process.env.SQL_UID,
    password: process.env.SQL_PWD,
    options: {
        encrypt: false,
        trustServerCertificate: true,
    }
};

// const db = {
//     server: "3.144.237.208",
//     database: "kukyflor",
//     user: "kuky",
//     password: "Kf123456",
//     options: {
//         encrypt: false,
//     },
//     pool: {
//         max: 100,
//         min: 0,
//         idleTimeoutMillis: 30000
//       }
// };

module.exports = {
    db
};