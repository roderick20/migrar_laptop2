const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.list = async function (UserId) {
        try {
            pool = await sql.connect(config.db);

            const query = `SELECT [Id]
                            ,[UniqueId]
                            ,[Nombre]
                            ,[Raza]
                            ,[Sexo]
                            ,[Color]
                            ,[Tamano]
                            ,[Caracteristicas]
                            ,[Peligroso]
                            ,[Creado]                            
                        FROM [dbo].[Mascota] WHERE [UserId] = @UserId`;

                        
            const request = pool.request();
            request.input('UserId', sql.Int, UserId);
            result = await request.query(query);
            console.log(result);
            return result.recordset;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
        finally {
            if (pool) {
                pool.close();
            }
        }
    }

    this.create = async function (params, UserId) {
        try {
console.log(params);
            pool = await sql.connect(config.db);

                const query = `INSERT INTO [dbo].[Mascota]
                (
                [Nombre]
                ,[Raza]
                ,[Sexo]
                ,[Color]
                ,[Tamano]
                ,[Caracteristicas]
                ,[Peligroso]
                ,[UserId])
          VALUES
                (
                @Nombre
                ,@Raza
                ,@Sexo
                ,@Color
                ,@Tamano
                ,@Caracteristicas
                ,@Peligroso
                ,@UserId) `;

            var request = new sql.Request();
            request.input('Nombre', sql.NVarChar, params.Nombre);
            request.input('Raza', sql.NVarChar, params.Raza);
            request.input('Sexo', sql.NVarChar, params.Sexo);
            request.input('Color', sql.NVarChar, params.Color);
            request.input('Tamano', sql.NVarChar, params.Tamano);
            request.input('Caracteristicas', sql.NVarChar, params.Caracteristicas);
            request.input('Peligroso', sql.NVarChar, params.Peligroso);

            request.input('UserId', sql.Int, UserId);

            result = await request.query(query);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    this.select = async function (uniquieId) {
        try {
            pool = await sql.connect(config.db);


            const query = `SELECT  
        [Id]
        ,[UniqueId]
        ,[Name]
        ,[Enabled]
        ,[Created]
        ,[Modified]
        FROM [dbo].[Company] WHERE [UniqueId] = '${uniquieId}'`;

            var request = new sql.Request();

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

            const query = `UPDATE [dbo].[Company] SET
            Name = @Name
    
            ,Enabled = @Enabled
            WHERE UniqueId = '${params.UniqueId}'`;

            var request = new sql.Request();
            request.input('Name', sql.NVarChar, params.Name);
            request.input('Enabled', sql.Bit, params.hasOwnProperty('Enabled') ? params.Enabled : false); 
            // request.input('Whatsapp', sql.Bit, company.Whatsapp);
            // request.input('Instagram', sql.Bit, company.Instagram);
            // request.input('Telegram', sql.Bit, company.Telegram);
            // request.input('Facebook', sql.Bit, company.Facebook);
            // request.input('Youtube', sql.Bit, company.Youtube);
            // request.input('TikTok', sql.Bit, company.TikTok);
            // request.input('Twitter', sql.Bit, company.Twitter);
            // request.input('Snapchat', sql.Bit, company.Snapchat);
            // request.input('Spotify', sql.Bit, company.Spotify);
            // request.input('Website', sql.Bit, company.Website);
            // request.input('Linkedin', sql.Bit, company.Linkedin);
            // request.input('Paypal', sql.Bit, company.Paypal);
            // request.input('PhoneNumber', sql.NVarChar, company.PhoneNumber);
            // request.input('Address', sql.NVarChar, company.Address);
            // request.input('FaceTime', sql.NVarChar, company.FaceTime);
            // request.input('Email', sql.NVarChar, company.Email);
            // request.input('Status', sql.Int, company.Status);

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

            const query = `DELETE FROM [dbo].[Company]
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