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
        const query = `SELECT UniqueId, Name, Email, Subject, 
                                CONVERT(varchar, Created, 101) + ' ' + RIGHT(CONVERT(varchar, Created, 100), 7) AS Created,
                                 Body, IsRead, 
                                CONVERT(varchar, ReadDate, 101) + ' ' + RIGHT(CONVERT(varchar, ReadDate, 100), 7) AS ReadDate,
                                Notifications, LastName, Phone
                        FROM dbo.Message;
                        `;

        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Message/list", err);
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

this.mensajePost = async function (Name, Email, Subject, Body, Phone) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `INSERT INTO dbo.Message (UniqueID, Name, Email, Subject, Created, Body, IsRead, ReadDate, Phone)
                        VALUES (NEWID(), @Name, @Email, @Subject, GETDATE() , @Body , 1, GETDATE(), @Phone);
                        `;
                    const request = pool.request();
                    request.input('Name', sql.NVarChar, Name);
                    request.input('Email', sql.NVarChar, Email);
                    request.input('Subject', sql.NVarChar, Subject);
                    request.input('Body', sql.NText, Body);
                    request.input('Phone', sql.NVarChar, Phone);
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

this.mensajeId = async function (UniqueId) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT UniqueId, Name, Email, Subject, 
                                CONVERT(varchar, Created, 101) + ' ' + RIGHT(CONVERT(varchar, Created, 100), 7) AS Created,
                                 Body, IsRead, 
                                CONVERT(varchar, ReadDate, 101) + ' ' + RIGHT(CONVERT(varchar, ReadDate, 100), 7) AS ReadDate,
                                Notifications, LastName, Phone
                        FROM dbo.Message
                        WHERE UniqueId = @UniqueId ;
                        `;

                    const request = pool.request();
                    request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                    result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Message/list", err);
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



 return this;

};