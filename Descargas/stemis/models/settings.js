const log = require('../utils/log.js');
const config = require('../utils/config.js');
const sql = require('mssql');
const crypto = require('crypto');

module.exports = function (app){
    
    this.listConf = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Title, [Key], Value, Type
                           FROM dbo.Settings
                           WHERE Type = 1;
                            `;
            result = await pool.request().query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Settings/listConf", err);
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

    this.listImage = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Title, [Key], Value, Type
                            FROM dbo.Settings
                            WHERE Type = 2;
                            `;
            result = await pool.request().query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Settings/listImages", err);
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
    
    this.listStyle = async function () {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Title, [Key], Value, Type
                            FROM dbo.Settings
                            WHERE Type = 3;
                            `;
            result = await pool.request().query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Settings/listStyles", err);
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

    this.listbyId = async function (UniqueId) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT UniqueId, Title, [Key], Value, Type
                            FROM dbo.Settings
                            WHERE UniqueId = @UniqueId;
                            `;
                        const request = pool.request();
                        request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                        result = await request.query(query);

        } catch (err) {
            error = err;
            log.logError("dbo.Settings/listStyles", err);
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

    this.settingUpdate = async function (UniqueId, Value) {
        let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
                UPDATE dbo.Settings
                SET  Value = @Value
                WHERE UniqueID = @UniqueID;
            `;
            const request = pool.request();
            request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
            request.input('Value', sql.NVarChar, Value);
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Settings/update", err);
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