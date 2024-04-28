const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.list = async function () {
        let pool;
        let result;
        let error = '';

        try {
            pool = await sql.connect(config.db);

            const query = `SELECT [nombre] FROM [dbo].[paises]`;

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