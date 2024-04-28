const log = require('../utils/log.js');
const config = require('../utils/config.js');
const sql = require('mssql');
const crypto = require('crypto');

module.exports = function (app){

    this.list = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Name, Email, Enabled,
                                    CONVERT(varchar, LastAccess, 101) + ' ' + RIGHT(CONVERT(varchar, LastAccess, 100), 7) AS LastAccess,
                                    Auth
                            FROM dbo.AuthUser;
                            `;
    
            
            result = await pool.request().query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.AuthUser/list", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
    
            error
        }
    }

    this.UsersId = async function (UniqueId) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Name, Email, Enabled, Password, 
                                    CONVERT(varchar, LastAccess, 101) + ' ' + RIGHT(CONVERT(varchar, LastAccess, 100), 7) AS LastAccess,
                                    Auth
                            FROM dbo.AuthUser
                            WHERE UniqueId = @UniqueId;
                            `;        
                    const request = pool.request();
                    request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                    result = await request.query(query);

        } catch (err) {
            error = err;
            log.logError("dbo.AuthUser/list", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
    
            error
        }
    }
    
    this.usersCreate = async function (Name, Email, Password, Enabled) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `INSERT INTO dbo.AuthUser (UniqueID, Name, Email, Password, LastAccess, Auth, Enabled)
                            VALUES (NEWID(), @Name, @Email, @Password, '2023-08-07 15:30:00', ' ' , @Enabled);
                            `;
                        const request = pool.request();
                        request.input('Name', sql.NVarChar, Name);
                        request.input('Email', sql.NVarChar, Email);
                        request.input('Password', sql.NVarChar, Password);
                        request.input('Enabled', sql.Bit, Enabled);
                        result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.AuthUser/list", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
    
            error
        }
    }
       
    this.usersUpdate = async function (UniqueID, Name, Email, Password, Enabled) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
                UPDATE dbo.AuthUser
                SET Name = @Name, Email = @Email, Password = @Password, Enabled = @Enabled
                WHERE UniqueID = @UniqueID;
            `;
            const request = pool.request();
            request.input('UniqueID', sql.UniqueIdentifier, UniqueID);
            request.input('Name', sql.NVarChar, Name);
            request.input('Email', sql.NVarChar, Email);
            request.input('Password', sql.NVarChar, Password);
            request.input('Enabled', sql.Bit, Enabled);
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.AuthUser/update", err);
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
    
    this.usersDelete = async function (UniqueID) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
                DELETE FROM dbo.AuthUser
                WHERE UniqueID = @UniqueID;
            `;
            const request = pool.request();
            request.input('UniqueID', sql.UniqueIdentifier, UniqueID);
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.AuthUser/delete", err);
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
    



    return this;

}