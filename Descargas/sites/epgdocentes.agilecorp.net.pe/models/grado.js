const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.create = async function (params, TeacherId) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `INSERT INTO [dbo].[Grados] ([Titulo],[Universidad],[Pais],[Anyo],[ResolucionSunedu],[Grado],[TeacherId])
                                VALUES (@Titulo,@Universidad,@Pais,@Anyo,@ResolucionSunedu,@Grado,@TeacherId)`;
            const request = pool.request();
            request.input('Grado', sql.NVarChar, params.Grado);
            request.input('Titulo', sql.NVarChar, params.Titulo);
            request.input('Universidad', sql.NVarChar, params.Universidad);
            request.input('Pais', sql.NVarChar, params.Pais);
            request.input('Anyo', sql.NVarChar, params.Anyo);
            request.input('ResolucionSunedu', sql.NVarChar, params.ResolucionSunedu);
            
            request.input('TeacherId', sql.Int, TeacherId);

            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.grado/create", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }

    this.delete = async function (params, TeacherId) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `DELETE FROM [dbo].[Grados] WHERE Id = @Id`;
            const request = pool.request();
            request.input('Id', sql.Int, params.Id);          

            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.grado/create", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }

    this.listByTeacherId = async function (TeacherId) {
        let pool;
        let result;
        let error = '';

        try {
            pool = await sql.connect(config.db);

            const query = `SELECT * FROM [dbo].[Grados] WHERE TeacherId = '${TeacherId}'`;

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

    return this;
}