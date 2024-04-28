const log = require('../utils/log.js');
const config = require('../utils/config.js');
const sql = require('mssql');

// login = async function (email, password) {
//     console.log('db login');
//     let pool;
//     let result;
//     let error = '';
//     try {
//         pool = await sql.connect(config.db);
//         const query = `
//              SELECT [Id],[UniqueId],[FirstName],[LastName],[Password],[LastAccess],[Status],[Email],[Role]
//             FROM [dbo].[Person] 
//             WHERE [Email] = @Email AND [Password] = ${password};
            
//             SELECT [Id],[UniqueId],[Name],[LastName],[Password],[LastAccess],[Enabled],[Email],[Role]
//             FROM [dbo].[User] 
//             WHERE [Email] = @Email AND [Password] = ${password};`;
            
//         const request = pool.request();
//         request.input('Email', sql.NVarChar, email);
//         request.input('Password', sql.NVarChar, params.password);
        
//         Execute the query and retrieve the result
//         result = await request.query(query);
       
     
//     } catch (err) {
//         error = err;
//         log.logError("user.js/login", err);
//     }
//     finally {
//         if (pool) {
//             pool.close();
//         }
//     }
//     return {
//         result,
//         error
//     };
// }
//Login con  MD5
login = async function (email, password) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query  = `
             SELECT  * FROM [dbo].[User] 
            WHERE [Email] = @Email AND [Password] = CONVERT(NVARCHAR(32), HashBytes('MD5', '${password}'), 2) AND Status = 2; 
            
           `;
        //    SELECT [Id], [UniqueId] ,[Name] ,[Email] ,[Password] ,[LastAccess] ,[Enabled] ,[Role] ,[Audit] ,[DNI]
        //    FROM [auth].[User]
        //    WHERE [Email] = @Email AND [Password] = CONVERT(NVARCHAR(32), HashBytes('MD5', '${password}'), 2);
        const request = pool.request();
        request.input('Email', sql.NVarChar, email);
        request.input('Password', sql.NVarChar, password);
        
        result = await request.query(query);
       
     
    } catch (err) {
        error = err;
        log.logError("user.js/login", err);
    }
    finally {
        if (pool) {
            pool.close();
        }
    }
    return {
        result,
        error
    };
}

module.exports = {
    login

};

