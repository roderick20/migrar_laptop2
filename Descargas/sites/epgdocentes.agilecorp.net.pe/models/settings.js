const log = require('../utils/log.js');
const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.select = async function (PersonId) {
        try {
            await sql.connect(config.db);

            const query = `
                SELECT *
                FROM dbo.Settings
                WHERE [PersonId] = @PersonId`;

            var request = new sql.Request();
            request.input('PersonId', sql.BigInt, PersonId); // Use parameterized query

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.selectProgramPerson = async function (PersonId) {
        try {
            await sql.connect(config.db);

            const query = `
                SELECT ProgramId
                FROM dbo.ProgramPerson
                WHERE [PersonId] = @PersonId`;

            var request = new sql.Request();
            request.input('PersonId', sql.BigInt, PersonId); // Use parameterized query

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.update = async function (person) {
        try {
            
            await sql.connect(config.db);
            const query = `UPDATE [dbo].[Settings]
                SET Image = @Image
                WHERE UniqueId = @UniqueId;`;
            var request = new sql.Request();
            request.input('PersonId', sql.BigInt, person.PersonId);
            request.input('Image', sql.NVarChar, person.Image);
            request.input('UniqueId', sql.UniqueIdentifier, person.UniqueId);
            result = await request.query(query);

            await sql.close();

            return result;
        } catch (err) {
            log.logError("dbo.Settings/update", err);
            throw err;
        }
    }

    this.deleted = async function (UniqueId) {
        try {
            await sql.connect(config.db);
    
            const query = `DELETE FROM [dbo].[Person]
            WHERE [UniqueId] = @UniqueId;
                `;
    
            var request = new sql.Request();
            request.input('UniqueId', sql.NVarChar, UniqueId); // Use the function parameter
            console.log(request);
    
            result = await request.query(query);
    
            await sql.close();
    
            return result;
        } catch (err) {
            log.logError("dbo.Person/delete", err);
            console.log(err);
            throw err;
        }
    }
    this.findAllDT = async function (params) {
        let pool, result, error = '';

        const draw = params.draw;
        const start = parseInt(params.start) == 0 ? 0 : parseInt(params.start) / params.length;
        const length = params.length;
        const search = params.search.value;

        try {
            pool = await sql.connect(config.db);
            const request = pool.request();
            let query = `SELECT COUNT(*) AS count FROM [dbo].[Settings]; `;
                query += `SELECT * FROM [dbo].[Settings]`;

            if (search) {
                query += ` WHERE [FirstName] LIKE @search `;
                request.input('search', sql.NVarChar, '%' + search + '%');
            }

            query += ` ORDER BY [FirstName] OFFSET @start ROWS FETCH NEXT @length ROWS ONLY;`;

            request.input('start', sql.Int, start * length);
            request.input('length', sql.Int, length);
            result = await request.query(query);
            console.log(result);
        } catch (err) {
            error = err;
            log.logError("persons/findAllDT", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            draw,
            recordsTotal: result.recordsets[0][0].count,
            recordsFiltered: result.recordsets[0][0].count,
            data: result.recordsets[1],
            error: error
        }
    }

    return this;

}