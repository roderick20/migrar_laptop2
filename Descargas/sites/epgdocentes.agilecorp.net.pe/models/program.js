const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.list = async function () {
        try {
            pool = await sql.connect(config.db);

            const query = `SELECT  
        [Id]
        ,[UniqueId]
        ,[Name]
        ,[Year]
        FROM [popl].[dbo].[Program]`;

            const request = pool.request();
            result = await request.query(query);
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
        finally {
            if (pool) {
                pool.close();
            }
        }
    }

    this.save = async function (params) {
        try {

            pool = await sql.connect(config.db);
                const query = `INSERT INTO [dbo].[Program] ( 
                    Name
                    ,Year
           
                    ) VALUES(
                         @Name
                        
                        ,@Year
                        ) `;

            var request = new sql.Request();
            request.input('Name', sql.NVarChar, params.Name);
            request.input('Year', sql.Int, params.Year);   
            result = await request.query(query);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    this.select = async function (uniqueid) {
        try {
            pool = await sql.connect(config.db);

            const query = `SELECT  *
        FROM [dbo].[Program] WHERE [UniqueId] = @UniqueId`;

            var request = new sql.Request();
            request.input('UniqueId', sql.UniqueIdentifier, uniqueid);

            result = await request.query(query);

            console.log(result);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.update = async function (params) {
        try {

            pool = await sql.connect(config.db);

            const query = `UPDATE [dbo].[Program] SET
            Name = @Name,
            Year = @Year
            WHERE UniqueId = @UniqueId`;

            var request = new sql.Request();
            request.input('UniqueId', sql.UniqueIdentifier, params.UniqueId);
            request.input('Name', sql.NVarChar, params.Name);
            request.input('Year', sql.Int, params.Year); 

            console.log(request);

            result = await request.query(query);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    this.deleted = async function (uniquieId) {
        try {

            pool = await sql.connect(config.db);

            const query = `DELETE FROM [dbo].[Program]
            WHERE [UniqueId] = '` + uniquieId + "' ";

            var request = new sql.Request();

            result = await request.query(query);

            console.log(result);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    return this;
}