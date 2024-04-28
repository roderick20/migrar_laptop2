// const log = require('../utils/log.js');
// const config = require('../utils/config.js');
// const sql = require('mssql');
// const crypto = require('crypto');


// async function read(params) {
//     let pool;
//     let result;
//     let error = '';
//     try {
//         pool = await sql.connect(config.db);

//         const query = `
//             SELECT [Id]
//                 ,[UniqueId]
//                 ,[Name]
//                 ,[Email]
//                 ,[Password]
//             FROM [dbo].[User] 
//             WHERE [Password] = @Password AND [Email] = @Email `;

//         const hash = crypto.createHash('sha256').update(params.password);
//         const passwordHex = hash.digest('hex');
    

//         const request = pool.request();
//         request.input('Email', sql.VarChar, params.email);
//         request.input('Password', sql.VarChar, passwordHex);
//         result = await request.query(query);
//     } catch (err) {
//         error = err;
//         log.logError("login.js/read", err);
//     }
//     finally {
//         if (pool) {
//             pool.close();
//         }
//     }

//     return {
//         result,
//         error
//     }
// }




// module.exports = {
//     read
// };