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
        const query = `SELECT Id, UniqueId, Name, Photo  FROM dbo.Colab;
                        `;
        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Colab/list", err);
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

this.typeColab = async function () {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT Id, UniqueId, Name
                       FROM dbo.ColabType;
                        `;
        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.ColabType/list", err);
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

this.postColab = async function (Name, ColabTypeId, Photo) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `INSERT INTO dbo.Colab(UniqueId, Name, Photo, ColabTypeId)
                        VALUES (NEWID(), @Name, '/uploads/'+@Photo, @ColabTypeId);
                        `;
                        const request = pool.request();
                        request.input('Name', sql.NVarChar, Name);
                        request.input('Photo', sql.NVarChar, Photo);
                        request.input('ColabTypeId', sql.Int, ColabTypeId);
                        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Colab/post", err);
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

this.colabDelete = async function (UniqueID) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `
            DELETE FROM dbo.Colab
            WHERE UniqueID = @UniqueID;
        `;
        const request = pool.request();
        request.input('UniqueID', sql.UniqueIdentifier, UniqueID);
        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Colab/delete", err);
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

this.listbyId = async function (UniqueId) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT Id, UniqueId, Name, Photo  
                        FROM dbo.Colab
                        WHERE UniqueId = @UniqueId;
                        `;
                        const request = pool.request();
                        request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Colab/list", err);
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

this.colabUpdate = async function(updateParams){
    console.log(updateParams)
    let pool;
        let result;
        let error = '';
        try {
            pool = await sql.connect(config.db);
            let updateFields = '';

            if(updateParams.Photo !== null){
                updateFields += `Photo = '/uploads/'+@Photo `;
            }
            
            let fieldsToUpdate = updateFields.trim(); // Eliminar espacios en blanco al final
            if (fieldsToUpdate) {
                fieldsToUpdate += ','; // Agregar coma al final si hay campos
            }

            const query = `
                UPDATE dbo.Colab
                SET Name = @Name,
                ${fieldsToUpdate} 
                ColabTypeId = @ColabTypeId
                WHERE UniqueId = @UniqueId;
            `;
            console.log(query); 
            const request = pool.request();
            request.input('UniqueId', sql.UniqueIdentifier, updateParams.UniqueId);
            request.input('Name', sql.NVarChar, updateParams.Name);
            request.input('ColabTypeId', sql.Int, updateParams.ColabTypeId);
            request.input('Photo', sql.NVarChar, updateParams.Photo);
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Colab/update", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return {
            result,
            error
        };
}

 return this;

};