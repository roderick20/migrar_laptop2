const log = require('../utils/log.js');
const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {


    this.list = async function () {
        try {
            await sql.connect(config.db);
            console.log('Conexión establecida con éxito');

            const query = `SELECT  [Id]
        ,[UniqueId]
        ,[CompanyId]
        ,[FirstName]
        ,[LastName]
        ,[Email]
        ,[Password]
        ,[Role]
        ,[Photo]
        ,[Address]
        ,[LastAccess]
        ,[Whatsapp]
        ,[Instagram]
        ,[Telegram]
        ,[Facebook]
        ,[Youtube]
        ,[TikTok]
        ,[Twitter]
        ,[Snapchat]
        ,[Spotify]
        ,[Website]
        ,[Linkedin]
        ,[Paypal]
        ,[PhoneNumber]
        ,[FaceTime]
        ,[Status]
        ,[Created]
        ,[Modified]
    FROM [popl].[dbo].[Person]`;

            var request = new sql.Request();

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.getDataCreate = async function () {

        try {
            await sql.connect(config.db);

            const query = `SELECT [Id], [Name] FROM [dbo].[Company]`;

            var request = new sql.Request();

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }

    }

    this.save = async function (person) {
        try {
            await sql.connect(config.db);
            const query = `INSERT INTO [dbo].[Person] (
            Id_area
            ,FirstName
            ,LastName
            ,Email
            ,[Password]
            ,Role          
            ,Photo
            ,Address
            ,Title
            ,JobTitle
            ,[Whatsapp]
            ,[WhatsappStatus]
            ,[Instagram]
            ,[InstagramStatus]
            ,[Telegram]
            ,[TelegramStatus]
            ,[Facebook]
            ,[FacebookStatus]
            ,[Youtube]
            ,[YoutubeStatus]
            ,[TikTok]
            ,[TikTokStatus]
            ,[Twitter]
            ,[TwitterStatus]
            ,[Snapchat]
            ,[SnapchatStatus]
            ,[Spotify]
            ,[SpotifyStatus]
            ,[Website]
            ,[WebsiteStatus]
            ,[Linkedin]
            ,[LinkedinStatus]
            ,[Paypal]
            ,[PaypalStatus]
            ,PhoneNumber
            ,Status) 
            OUTPUT INSERTED.Id 
            VALUES(
                @Id_area
                ,@FirstName
                ,@LastName
                ,@Email
                ,CONVERT(NVARCHAR(32), HashBytes('MD5', '${person.Password}'), 2)
                ,@Role               
                ,@Photo
                ,@Address
                ,@Title
                ,@JobTitle
                ,@Whatsapp
                ,@WhatsappStatus
                ,@Instagram
                ,@InstagramStatus
                ,@Telegram
                ,@TelegramStatus
                ,@Facebook
                ,@FacebookStatus
                ,@Youtube
                ,@YoutubeStatus
                ,@TikTok
                ,@TikTokStatus
                ,@Twitter
                ,@TwitterStatus
                ,@Snapchat
                ,@SnapchatStatus
                ,@Spotify
                ,@SpotifyStatus
                ,@SitioWeb
                ,@WebsiteStatus
                ,@Linkedin
                ,@LinkedinStatus
                ,@Paypal
                ,@PaypalStatus
                ,@PhoneNumber
                ,@Status) `;

            var request = new sql.Request();
            request.input('Id_area', sql.Int, person.Id_area);
            request.input('FirstName', sql.NVarChar, person.FirstName);
            request.input('LastName', sql.NVarChar, person.LastName);
            request.input('Email', sql.NVarChar, person.Email);
            request.input('Password', sql.NVarChar, person.Password);
            request.input('Role', sql.Int, 1);//person.Role);
            request.input('Photo', sql.NVarChar, person.Photo);
            request.input('Address', sql.NVarChar, person.Address);
            request.input('Title', sql.NVarChar, person.Title);
            request.input('JobTitle', sql.NVarChar, person.JobTitle);
            request.input('Whatsapp', sql.NVarChar, person.Whatsapp);
            request.input('WhatsappStatus', sql.Bit, person.WhatsappStatus == undefined ? 0 : 1);
            request.input('Instagram', sql.NVarChar, person.Instagram);
            request.input('InstagramStatus', sql.Bit, person.InstagramStatus == undefined ? 0 : 1);
            request.input('Telegram', sql.NVarChar, person.Telegram);
            request.input('TelegramStatus', sql.Bit, person.TelegramStatus == undefined ? 0 : 1);
            request.input('Facebook', sql.NVarChar, person.Facebook);
            request.input('FacebookStatus', sql.Bit, person.FacebookStatus == undefined ? 0 : 1);
            request.input('Youtube', sql.NVarChar, person.Youtube);
            request.input('YoutubeStatus', sql.Bit, person.YoutubeStatus == undefined ? 0 : 1);
            request.input('TikTok', sql.NVarChar, person.TikTok);
            request.input('TikTokStatus', sql.Bit, person.TikTokStatus == undefined ? 0 : 1);
            request.input('Twitter', sql.NVarChar, person.Twitter);
            request.input('TwitterStatus', sql.Bit, person.TwitterStatus == undefined ? 0 : 1);
            request.input('Snapchat', sql.NVarChar, person.Snapchat);
            request.input('SnapchatStatus', sql.Bit, person.SnapchatStatus == undefined ? 0 : 1);
            request.input('Spotify', sql.NVarChar, person.Spotify);
            request.input('SpotifyStatus', sql.Bit, person.SpotifyStatus == undefined ? 0 : 1);
            request.input('SitioWeb', sql.NVarChar, person.SitioWeb);
            request.input('WebsiteStatus', sql.Bit, person.WebsiteStatus == undefined ? 0 : 1);
            request.input('Linkedin', sql.NVarChar, person.Linkedin);
            request.input('LinkedinStatus', sql.Bit, person.LinkedinStatus == undefined ? 0 : 1);
            request.input('Paypal', sql.NVarChar, person.Paypal);
            request.input('PaypalStatus', sql.Bit, person.PaypalStatus == undefined ? 0 : 1);
            request.input('PhoneNumber', sql.NVarChar, person.PhoneNumber);
            request.input('FaceTime', sql.NVarChar, person.FaceTime);
            request.input('Status', sql.Bit, person.Status == undefined ? 0 : 1);
            result = await request.query(query);

            const insertedId = result.recordset[0].Id;
            const insertSettingsRequestQuery = `
            INSERT INTO [dbo].[Settings]
            ([PersonId])
            VALUES
            (@PersonId);
        `;

                const insertSettingsRequest = new sql.Request();
                insertSettingsRequest.input('PersonId', sql.BigInt, insertedId);

                await insertSettingsRequest.query(insertSettingsRequestQuery);

                if (person.ProgramId && person.ProgramId.length > 0) {
                    for (const ProgramId of person.ProgramId) {
                      const insertProgramPerson = `
                        INSERT INTO [dbo].[ProgramPerson]
                        ([ProgramId], [PersonId])
                        VALUES
                        (@ProgramId, @PersonId);
                      `;
                  
                      const insertProgramPersonRequest = new sql.Request();
                  
                      // Ajusta los tipos de datos según tus necesidades
                      insertProgramPersonRequest.input('PersonId', sql.BigInt, insertedId);
                      insertProgramPersonRequest.input('ProgramId', sql.BigInt, ProgramId);
                  
                      try {
                        // Ejecutar la inserción
                        await insertProgramPersonRequest.query(insertProgramPerson);
                      } catch (err) {
                        console.error('Error al insertar en ProgramPerson:', err);
                        // Manejar el error según tus necesidades
                      }
                    }
                  }
                  
            
            


            await sql.close();

            return result;
        } catch (err) {
             log.logError("dbo.Settings/create", err);
            throw err;
        }
    }

    this.select = async function (UniqueId) {
        try {
            await sql.connect(config.db);

            const query = `
                SELECT [Id]
                ,[UniqueId]
                ,[CompanyId]
                ,[FirstName]
                ,[LastName]
                ,[Email]
                ,[Password]
                ,[Role]
                ,[Photo]
                ,[Address]
                ,[Title]
                ,[JobTitle]
                ,[LastAccess]
                ,[Whatsapp]
                ,[WhatsappStatus]
                ,[Instagram]
                ,[InstagramStatus]
                ,[Telegram]
                ,[TelegramStatus]
                ,[Facebook]
                ,[FacebookStatus]
                ,[Youtube]
                ,[YoutubeStatus]
                ,[TikTok]
                ,[TikTokStatus]
                ,[Twitter]
                ,[TwitterStatus]
                ,[Snapchat]
                ,[SnapchatStatus]
                ,[Spotify]
                ,[SpotifyStatus]
                ,[Website]
                ,[WebsiteStatus]
                ,[Linkedin]
                ,[LinkedinStatus]
                ,[Paypal]
                ,[PaypalStatus]
                ,[PhoneNumber]
                ,[FaceTime]
                ,[Status]
                ,[Created]
                ,[Modified]
                ,[Id_area]
                FROM [popl].[dbo].[Person]
                WHERE [UniqueId] = @UniqueId`;

            var request = new sql.Request();
            request.input('UniqueId', sql.NVarChar, UniqueId); // Use parameterized query

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }
    this.SelectArea = async function (UniqueId) {
        try {
            await sql.connect(config.db);
            const query = `
            select * from dbo.Area;`;

            var request = new sql.Request();
            request.input('UniqueId', sql.NVarChar, UniqueId); // Use parameterized query

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
            let updateFields = '';
            for (const prop in person) {
                if (person[prop] !== undefined && prop !== 'UniqueId' && !prop.endsWith('Status') && prop !== 'ProgramId' && prop !== 'PersonId') {
                    updateFields += `${prop} = @${prop}, `;
                }
            }
    
            // Agregar el bucle para campos de status
            for (const prop in person) {
                if (prop.endsWith('Status')) {
                    updateFields += `${prop} = @${prop}, `;
                }
            }

            updateFields = updateFields.slice(0, -2);

            const query = `UPDATE [dbo].[Person]
                SET ${updateFields}
                WHERE UniqueId = @UniqueId;`;


            var request = new sql.Request();
            request.input('Id_area', sql.Int, person.Id_area);
            request.input('UniqueId', sql.NVarChar, person.UniqueId);
            request.input('FirstName', sql.NVarChar, person.FirstName);
            request.input('LastName', sql.NVarChar, person.LastName);
            request.input('Email', sql.NVarChar, person.Email);
            request.input('Password', sql.NVarChar, person.Password);
            request.input('Role', sql.Int, person.Role);
            request.input('Photo', sql.NVarChar, person.Photo);
            request.input('Address', sql.NVarChar, person.Address);
            request.input('LastAccess', sql.DateTime, person.LastAccess);
            request.input('Title', sql.NVarChar, person.Title);
            request.input('JobTitle', sql.NVarChar, person.JobTitle);
            request.input('Whatsapp', sql.NVarChar, person.Whatsapp);
            request.input('WhatsappStatus', sql.Bit, person.WhatsappStatus == undefined ? 0 : 1);
            request.input('Instagram', sql.NVarChar, person.Instagram);
            request.input('InstagramStatus', sql.Bit, person.InstagramStatus == undefined ? 0 : 1);
            request.input('Telegram', sql.NVarChar, person.Telegram);
            request.input('TelegramStatus', sql.Bit, person.TelegramStatus == undefined ? 0 : 1);
            request.input('Facebook', sql.NVarChar, person.Facebook);
            request.input('FacebookStatus', sql.Bit, person.FacebookStatus == undefined ? 0 : 1);
            request.input('Youtube', sql.NVarChar, person.Youtube);
            request.input('YoutubeStatus', sql.Bit, person.YoutubeStatus == undefined ? 0 : 1);
            request.input('TikTok', sql.NVarChar, person.TikTok);
            request.input('TikTokStatus', sql.Bit, person.TikTokStatus == undefined ? 0 : 1);
            request.input('Twitter', sql.NVarChar, person.Twitter);
            request.input('TwitterStatus', sql.Bit, person.TwitterStatus == undefined ? 0 : 1);
            request.input('Snapchat', sql.NVarChar, person.Snapchat);
            request.input('SnapchatStatus', sql.Bit, person.SnapchatStatus == undefined ? 0 : 1);
            request.input('Spotify', sql.NVarChar, person.Spotify);
            request.input('SpotifyStatus', sql.Bit, person.SpotifyStatus == undefined ? 0 : 1);
            request.input('Website', sql.NVarChar, person.Website);
            request.input('WebsiteStatus', sql.Bit, person.WebsiteStatus == undefined ? 0 : 1);
            request.input('Linkedin', sql.NVarChar, person.Linkedin);
            request.input('LinkedinStatus', sql.Bit, person.LinkedinStatus == undefined ? 0 : 1);
            request.input('Paypal', sql.NVarChar, person.Paypal);
            request.input('PaypalStatus', sql.Bit, person.PaypalStatus == undefined ? 0 : 1);
            request.input('PhoneNumber', sql.NVarChar, person.PhoneNumber);
            request.input('FaceTime', sql.NVarChar, person.FaceTime);
            request.input('Status', sql.Bit, person.Status == undefined ? 0 : 1);

            result = await request.query(query);

            if(person.ProgramId && person.ProgramId.length > 0){
                //Eliminar datos program person
                const deletequery = `
                DELETE FROM dbo.ProgramPerson
                WHERE PersonId = @PersonId;
                    `;
        
                var deletedrequest = new sql.Request();
                deletedrequest.input('PersonId', sql.BigInt, person.PersonId); 
        
                result = await deletedrequest.query(deletequery);

                    for (const ProgramId of person.ProgramId) {
                      const insertProgramPerson = `
                        INSERT INTO [dbo].[ProgramPerson]
                        ([ProgramId], [PersonId])
                        VALUES
                        (@ProgramId, @PersonId);
                      `;
                  
                      const insertProgramPersonRequest = new sql.Request();
                  
                      // Ajusta los tipos de datos según tus necesidades
                      insertProgramPersonRequest.input('PersonId', sql.BigInt,  person.PersonId);
                      insertProgramPersonRequest.input('ProgramId', sql.BigInt, ProgramId);
                  
                      try {
                        // Ejecutar la inserción
                        await insertProgramPersonRequest.query(insertProgramPerson);
                      } catch (err) {
                        console.error('Error al insertar en ProgramPerson:', err);
                        // Manejar el error según tus necesidades
                      }
                    }
                  
            }

            await sql.close();

            return result;
        } catch (err) {
            log.logError("dbo.Person/update", err);
            throw err;
        }
    }

    this.deleted = async function (UniqueId, PersonId) {
        try {
            await sql.connect(config.db);
    
            const query = `DELETE FROM [dbo].[Person]
            WHERE [UniqueId] = @UniqueId;

            DELETE FROM dbo.Settings
            WHERE PersonId = @PersonId;

            DELETE FROM dbo.ProgramPerson
            WHERE PersonId = @PersonId;
                `;
    
            var request = new sql.Request();
            request.input('UniqueId', sql.UniqueIdentifier, UniqueId); // Use the function parameter
            request.input('PersonId', sql.BigInt, PersonId); 
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
            let query = `SELECT COUNT(*) AS count FROM [dbo].[Person]; `;
                query += `SELECT * FROM [dbo].[Person]`;

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

    this.findAllDTone = async function (params) {
        let pool, result, error = '';

        const draw = params.draw;
        const start = parseInt(params.start) == 0 ? 0 : parseInt(params.start) / params.length;
        const length = params.length;
        const search = params.search.value;

        try {
            pool = await sql.connect(config.db);
            const request = pool.request();
            let query = `SELECT COUNT(*) AS count FROM [dbo].[Person]; `;
                query += `SELECT * FROM [dbo].[Person]`;
                query += ` WHERE [Id] LIKE @PersonId `;
        


            query += ` ORDER BY [FirstName] OFFSET @start ROWS FETCH NEXT @length ROWS ONLY;`;

            request.input('PersonId', sql.BigInt, params.PersonId);
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

    this.updatePassword = async function (params, UniqueId) {
        console.log(params.Password, UniqueId)
        try {
            
            await sql.connect(config.db);

            const query = `UPDATE [dbo].[Person]
                SET [Password] = CONVERT(NVARCHAR(32), HashBytes('MD5', '${params.Password}'), 2)
                WHERE UniqueId = @UniqueId;`;


            var request = new sql.Request();
            request.input('Password', sql.NVarChar, params.Password);
            request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
          
            result = await request.query(query);

        

            await sql.close();

            return result;
        } catch (err) {
            log.logError("dbo.Person/updatepassword", err);
            throw err;
        }
    }

    return this;

}