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
        ,[Enabled]
        FROM [popl].[dbo].[Company]`;

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

            // const query = `INSERT INTO [dbo].[Company] ( 
            // Name
            // ,Whatsapp
            // ,Instagram
            // ,Telegram
            // ,Facebook
            // ,Youtube
            // ,TikTok
            // ,Twitter
            // ,Snapchat
            // ,Spotify
            // ,Website
            // ,Linkedin
            // ,Paypal
            // ,PhoneNumber
            // ,Address
            // ,FaceTime
            // ,Email
            // ,Status
            // ) VALUES(
            //      @Name
            //     ,@Whatsapp
            //     ,@Instagram
            //     ,@Telegram
            //     ,@Facebook
            //     ,@Youtube
            //     ,@TikTok
            //     ,@Twitter
            //     ,@Snapchat
            //     ,@Spotify
            //     ,@Website
            //     ,@Linkedin
            //     ,@Paypal
            //     ,@PhoneNumber
            //     ,@Address
            //     ,@FaceTime
            //     ,@Email
            //     ,@Status
            //     ) `;

                const query = `INSERT INTO [dbo].[Company] ( 
                    Name
                    ,Enabled
           
                    ) VALUES(
                         @Name
                        
                        ,@Enabled
                        ) `;

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