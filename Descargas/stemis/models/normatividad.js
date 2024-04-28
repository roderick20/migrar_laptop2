const log = require('../utils/log.js');
const config = require('../utils/config.js');
const sql = require('mssql');
const crypto = require('crypto');

module.exports = function (app){
//Homepage
this.list = async function () {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT UniqueId, Title, Summary, Tags, Slug, CardPhoto, Photo
                        FROM dbo.Normativity;
                        `;
        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/list", err);
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
         
this.NormId = async function (Slug) {
    let pool;
    let result;
    let error = '';
    
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT UniqueId, Title, Slug, Body, Tags, PDFile, Photo, CardPhoto
                        FROM dbo.Normativity
                        WHERE Slug = @Slug;
                        `;
         const request = pool.request();
        
            request.input('Slug', sql.NVarChar, Slug);
    //         request.input('Password', sql.VarChar, passwordHex);
            result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/list", err);
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

this.search = async function (palabraClave) {
    let pool;
    let result;
    let error = '';
    
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT UniqueId, Title, Summary, Tags, Slug, CardPhoto, Photo
                                FROM dbo.Normativity
                                    WHERE Title LIKE '%' + @palabraClave + '%'
                                    OR Summary LIKE '%' + @palabraClave + '%';
                        `;
         const request = pool.request();
        
            request.input('palabraClave', sql.NVarChar, palabraClave);
    //         request.input('Password', sql.VarChar, passwordHex);
            result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Blogpost/list", err);
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
//AdminPage
this.normCreate = async function (publishValue,Title, Slug, Tags, Summary, Body, PDFile, Photo, CardPhoto){
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = ` INSERT INTO dbo.Normativity(UniqueId,Title,Slug, Body, Summary, Tags, Auth, IsPublish, PDFile, 
                            Photo, CardPhoto)
                        VALUES ( NEWID(), @Title, @Slug, @Body, @Summary, @Tags, 1, @publishValue, '/uploads/'+@PDFile, '/uploads/'+@Photo, '/uploads/'+@CardPhoto);
                        `;
                    const request = pool.request();
                    request.input('Title', sql.NVarChar, Title);
                    request.input('Slug', sql.NVarChar, Slug);
                    request.input('Body', sql.NText, Body);
                    request.input('Summary', sql.NText, Summary);
                    request.input('publishValue', sql.Bit, publishValue);
                    request.input('Tags', sql.NVarChar, Tags);
                    request.input('Photo', sql.NVarChar, Photo);
                    request.input('PDFile', sql.NVarChar, PDFile);
                    request.input('CardPhoto', sql.NVarChar, CardPhoto);
                    result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/create", err);
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

this.normsbyId = async function (UniqueId){
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT dbo.Normativity.UniqueId, Title, IsPublish, Slug, Tags, Summary, Body, PDFile, Photo, CardPhoto
                        FROM dbo.Normativity
                        WHERE dbo.Normativity.UniqueId = @UniqueId;
                        `;        
                const request = pool.request();
                request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                result = await request.query(query);

    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/normId", err);
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

this.normDelete = async function (UniqueID) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `
            DELETE FROM dbo.Normativity
            WHERE UniqueID = @UniqueID;
        `;
        const request = pool.request();
        request.input('UniqueID', sql.UniqueIdentifier, UniqueID);
        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/delete", err);
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

this.normUpdate = async function (updateParams) {
    console.log(updateParams)
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        let updateFields = '';
        const inputParams = {
            UniqueId: sql.UniqueIdentifier,
            IsPublish: sql.Bit,
            Title: sql.NVarChar,
            Slug: sql.NVarChar,
            Body: sql.NText,
            Summary: sql.NText,
            Tags: sql.NVarChar,
            PDFile: sql.NVarChar,
            Photo: sql.NVarChar,
            CardPhoto: sql.NVarChar
        };
        // Verificacion de campos nulos
        if (updateParams.IsPublish !== undefined){
            updateFields += `IsPublish = 1, `;
        }else{
            updateFields += `IsPublish = 0, `;
        }
        if (updateParams.Title !== undefined) {
            updateFields += 'Title = @Title, ';
        }
        if (updateParams.Slug !== undefined) {
            updateFields += 'Slug = @Slug, ';
        }
        if (updateParams.Body !== undefined) {
            updateFields += 'Body = @Body, ';
        }
        if (updateParams.Summary !== undefined) {
            updateFields += 'Summary = @Summary, ';
        }
        if (updateParams.Tags !== undefined) {
            updateFields += 'Tags = @Tags, ';
        }
        if (updateParams.PDFile !== null) {
            updateFields += `PDFile = '/uploads/'+@PDFile, `;
        }
        if (updateParams.Photo !== null) {
            updateFields += `Photo = '/uploads/'+@Photo, `;
        }
        if (updateParams.CardPhoto !== null) {
            updateFields += `CardPhoto = '/uploads/'+@CardPhoto, `;
        }

        updateFields = updateFields.trim();
        if (updateFields.endsWith(',')) {
            updateFields = updateFields.slice(0, -1);
        }

        const query = `
            UPDATE dbo.Normativity
            SET ${updateFields}
            WHERE UniqueID = @UniqueID;
        `;
        const request = pool.request();
        request.input('UniqueId', inputParams.UniqueId, updateParams.UniqueId);
        request.input('IsPublish', inputParams.IsPublish, updateParams.IsPublish);
        request.input('Title', inputParams.Title, updateParams.Title);
        request.input('Slug', inputParams.Slug, updateParams. Slug);
        request.input('Body', inputParams.Body, updateParams.Body);
        request.input('Summary', inputParams.Summary, updateParams.Summary);
        request.input('Tags', inputParams.Tags, updateParams.Tags);
        request.input('PDFile', inputParams.PDFile, updateParams.PDFile);
        request.input('Photo', inputParams.Photo, updateParams.Photo);
        request.input('CardPhoto', inputParams.CardPhoto, updateParams.CardPhoto);

        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Normativity/update", err);
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

};