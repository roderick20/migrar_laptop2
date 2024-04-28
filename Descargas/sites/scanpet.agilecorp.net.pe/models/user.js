const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.list = async function () {
        let pool;
        let result;
        let error = '';

        try {
            pool = await sql.connect(config.db);

            const query = `SELECT  [Id]
        ,[UniqueId]
        ,[Name]
        ,[LastName]
        ,[Email]
        ,[Password]
        ,[LastAccess]
        ,[Enabled]
        ,[Role]
        ,[Created]
        ,[Modified]


    FROM [dbo].[User]`;

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

    this.create = async function (user) {
        try {
console.log(user);
            await sql.connect(config.db);

            const query = `INSERT INTO [dbo].[User]
            (
            [Name]
            ,[LastName]     
            ,[Email]
            ,[Password]           
            ,[Status]           
            ,[codeAuth])
      VALUES
            (
            @Name
            ,@LastName        
            ,@Email
            ,CONVERT(NVARCHAR(32), HashBytes('MD5', '${user.password}'), 2)
            ,@Status         
            ,@codeAuth)`;

            var request = new sql.Request();
            request.input('Name', sql.NVarChar, user.nameContact);
            request.input('LastName', sql.NVarChar, user.lastnameContact);
            request.input('Email', sql.NVarChar, user.email);
            request.input('Status', sql.Int, 1);
            request.input('codeAuth', sql.NVarChar, user.codeAuth);

            //console.log(request);

            result = await request.query(query);

            //console.log(result);

            await sql.close();

            return result;
        } catch (err) {
            console.log(err);
            throw err;
        }
    }

    this.save = async function (user) {
        try {

            await sql.connect(config.db);

            const query = `INSERT INTO [dbo].[User] ( 
             Name
            ,LastName
            ,Email
            ,Password
            ,Enabled
            ,Role
            ) VALUES(@Name
                ,@LastName
                ,@Email
                ,CONVERT(NVARCHAR(32), HashBytes('MD5', '${user.Password}'), 2)
                ,@Enabled
                ,@Role) `;

            var request = new sql.Request();
            request.input('Name', sql.NVarChar, user.Name);
            request.input('LastName', sql.NVarChar, user.LastName);
            request.input('Email', sql.NVarChar, user.Email);
            request.input('Password', sql.NVarChar, user.Password);
            request.input('Enabled', sql.Bit, params.hasOwnProperty('Enabled') ? params.Enabled : false);
            request.input('Role', sql.Int, user.Role);

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

    this.select = async function (uniquieId) {
        try {
            await sql.connect(config.db);

            console.log('select ' + uniquieId);

            const query = `SELECT  
        [Id]
        ,[UniqueId]
        ,[Name]
        ,[LastName]
        ,[Email]
        ,[Password]
        ,[LastAccess]
        ,[Enabled]
        ,[Role]
        ,[Created]
        ,[Modified]
        FROM [dbo].[User] WHERE [UniqueId] = '` + uniquieId + `' `;

            var request = new sql.Request();

            result = await request.query(query);

            await sql.close();

            return result;

        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.validarCuenta  = async function (codeAuth) {
        try {

            console.log(codeAuth);

            pool = await sql.connect(config.db);

            const query = `UPDATE [dbo].[User] SET [Status] = 2 WHERE [codeAuth] =  @codeAuth`;
            const request = pool.request();

            request.input('codeAuth', sql.NVarChar, codeAuth);
  
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

    this.recoverypassword  = async function (params) {
        try {

            console.log(params);

            pool = await sql.connect(config.db);

            const query = `UPDATE [dbo].[User] SET [Password] = CONVERT(NVARCHAR(32), HashBytes('MD5', '${params.password}'), 2) WHERE [email] =  @email`;
            const request = pool.request();

            request.input('email', sql.NVarChar, params.email);
            
  
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

    this.update = async function (params) {
        try {

            console.log(params);

            pool = await sql.connect(config.db);

            const query = `UPDATE [dbo].[User] SET 
             Name = @Name
            ,LastName = @LastName
            ,Email = @Email          
            ,Enabled = @Enabled
            WHERE UniqueId = '${params.UniqueId}'`;
            const request = pool.request();

            request.input('Name', sql.NVarChar, params.Name);
            request.input('LastName', sql.NVarChar, params.LastName);
            request.input('Email', sql.NVarChar, params.Email);
            request.input('Enabled', sql.Bit, params.hasOwnProperty('Enabled') ? params.Enabled : false);
            //request.input('UniqueId', sql.NVarChar, params.UniqueId); 
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

    this.deleted = async function (uniquieId) {
        try {

            await sql.connect(config.db);

            const query = `DELETE FROM [dbo].[User]
            WHERE [UniqueId] = '` + uniquieId + "' ";

            var request = new sql.Request();

            console.log(request);

            result = await request.query(query);

            await sql.close();

            return result;
        } catch (err) {
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
            let query = `SELECT COUNT(*) AS count FROM [dbo].[User]; `;
            query += `SELECT [Id],[UniqueId],[Name],[LastName],[Email],[Password],[LastAccess],[Enabled],[Role], FORMAT([Created], 'dd/MM/yyyy') AS Created, [Modified]
            FROM [dbo].[User] `;

            if (search) {
                query += ` WHERE [Name] LIKE @search `;
                request.input('search', sql.NVarChar, '%' + search + '%');
            }

            query += ` ORDER BY [Name] OFFSET @start ROWS FETCH NEXT @length ROWS ONLY;`;

            request.input('start', sql.Int, start * length);
            request.input('length', sql.Int, length);
            result = await request.query(query);
            console.log(result);
        } catch (err) {
            error = err;
            log.logError("users/findAllDT", err);
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

            const query = `UPDATE [dbo].[User]
                SET [Password] = CONVERT(NVARCHAR(32), HashBytes('MD5', '${params.Password}'), 2)
                WHERE UniqueId = @UniqueId;`;


            var request = new sql.Request();
            request.input('Password', sql.NVarChar, params.Password);
            request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
          
            result = await request.query(query);

        

            await sql.close();

            return result;
        } catch (err) {
            log.logError("dbo.User/updatepassword", err);
            throw err;
        }
    }


    return this;
}