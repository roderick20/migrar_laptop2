const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.list = async function () {
        let pool;
        let result;
        let error = '';

        try {
            pool = await sql.connect(config.db);

            const query = `SELECT A.Id_area, A.Id_usuario, A.Area, ISNULL(U.FirstName, 'No hay personal') AS NombreUsuario
            FROM [popl].[dbo].[Area] AS A
            LEFT JOIN [popl].[dbo].[Person] AS U ON A.Id_usuario = U.Id;
            `;

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

    this.save = async function (area) {
        try {

            await sql.connect(config.db);

            const query = `INSERT INTO [Area] 
            (Area)
            VALUES (@area); `;

            var request = new sql.Request();
            // request.input('Id_area', sql.Int, area.Id_area);
            request.input('area', sql.VarChar, area.area);

            console.log(request);

            result = await request.query(query);

            console.log(result);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    this.select = async function (Id_area) {
        try {
            const pool = await sql.connect(config.db);
            console.log('select ' + Id_area);

            const query = `
                SELECT  
                [Id_area]
                ,[Area]
                ,[Id_usuario]
                FROM [popl].[dbo].[Area] WHERE [Id_Area] = @Id_area;
            `;

            const request = pool.request();
            request.input('Id_area', sql.Int, Id_area);
            const result = await request.query(query);
            //console.log(result);

            sql.close(); // Cerrar la conexión después de obtener el resultado

            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
            throw err; // Relanzar el error para que pueda ser manejado fuera de la función
        }
    }

    this.listusuario = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
            SELECT [Id] as value , FirstName as text FROM [dbo].[Person];
            `;
            const request = pool.request();
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Receta/delete", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
            error
        };
    };
    this.listarea = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
            SELECT [Id_area] as value , [Area] as text FROM [dbo].[Area];
            `;
            const request = pool.request();
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Receta/delete", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
            error
        };
    };


    this.update = async function (params) {
        try {

            console.log(params);

            pool = await sql.connect(config.db);

            const query = `UPDATE [dbo].[Area] SET 
             Id_area = @Id_area
            ,Id_usuario = @Id_usuario
            ,Area = @Area          
            WHERE Id_area = @Id_area;
            UPDATE [dbo].[Person] SET
            Id_area = @Id_area
            WHERE Id = @Id_usuario`;
            const request = pool.request();

            request.input('Id_area', sql.NVarChar, params.Id_area);
            request.input('Id_usuario', sql.NVarChar, params.Id_usuario);
            request.input('Area', sql.NVarChar, params.Area);
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

    this.deleted = async function (UniqueId) {
        try {
            const pool = await sql.connect(config.db);
            const query = `DELETE FROM [dbo].[Area] WHERE [Id_area] = @UniqueId`;
    
            const request = pool.request();
            request.input('UniqueId', sql.NVarChar, UniqueId);
    
            const result = await request.query(query);
    
            await sql.close();
    
            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    return this;
}