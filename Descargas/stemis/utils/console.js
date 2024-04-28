const dotenv = require('dotenv');
dotenv.config();

// const userModel = require('./app/models/user.js');

// async function test (){
// const users = await userModel.listdt({ draw: 1, start: 1, length: 2 } );
// console.log(users);
// }
// test();

const sql = require('mssql');

const db = {
    server: "3.144.237.208",
    database: "kflor",
    user: "kuky",
    password: "Kf123456",
    // connectionTimeout: 3000,
    // requestTimeout: 20000,
    options: {
        encrypt: false,
        trustServerCertificate: true,
    },
    // pool: {
    //     max: 100,
    //     min: 0,
    //     idleTimeoutMillis: 30000
    // }
};

async function list() {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(db);
        const query = `
            SELECT [id]
            ,[uniqueId]
            ,[name]
            ,[email]
            ,[password]
            ,[author]
            ,[created]
            ,[modified]
            ,[editor]
            FROM [dbo].[user];`;
        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        console.log(err);
        //log.logError("user.js/list", err);
    }
    finally {
        if (pool) {
            pool.close();
        }
    }
    return {
        result,
        error
    }
}

list();