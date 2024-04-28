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
        const query = `SELECT dbo.BlogPost.UniqueId, dbo.BlogPost.Title, CardSummary, Imagen, Slug, dbo.BlogCategory.Title AS TitleCat, 
                                CONVERT(varchar, PostDate, 101) + ' ' + RIGHT(CONVERT(varchar, PostDate, 100), 7) AS PostDate,
                                PostAuthor, Tags
                        FROM dbo.BlogPost
                        INNER JOIN dbo.BlogCategory ON dbo.BlogPost.CategoryId = dbo.BlogCategory.Id;
                        `;

        
        result = await pool.request().query(query);
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

this.listByTag = async function (tag) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT dbo.Subject.Name ,Title, CardSummary, Imagen, Slug, 
                                CONVERT(varchar, PostDate, 101) + ' ' + RIGHT(CONVERT(varchar, PostDate, 100), 7) AS PostDate,
                                PostAuthor, Tags
                        FROM dbo.BlogPost 
                        INNER JOIN dbo.Subject ON dbo.BlogPost.SubjectId = dbo.Subject.Id
                        WHERE dbo.Subject.Name = @tag;
                        `;
         const request = pool.request();
         request.input('tag', sql.NVarChar, tag);
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

this.PostId = async function (Slug) {
    let pool;
    let result;
    let error = '';
    
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT dbo.Colab.Photo, Title, Imagen, Body, Summary, Tags, PhotoAuthor, Background, PostAuthor,  CONVERT(varchar, PostDate, 101) + ' ' + RIGHT(CONVERT(varchar, PostDate, 100), 7) AS PostDate,
                        Judgement, Pleno, Expedient, CardSummary, PDFile
                        FROM dbo.BlogPost 
                        INNER JOIN dbo.Colab ON dbo.BlogPost.ColabId = dbo.Colab.Id
                        WHERE Slug = @Slug;
                        `;
         const request = pool.request();
        
            request.input('Slug', sql.NVarChar, Slug);
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

this.search = async function (palabraClave) {
    let pool;
    let result;
    let error = '';
    
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT UniqueId, Title, CardSummary, Imagen, Slug, 
                        CONVERT(varchar, PostDate, 101) + ' ' + RIGHT(CONVERT(varchar, PostDate, 100), 7) AS PostDate,
                            PostAuthor, Tags 
                                FROM dbo.BlogPost
                                    WHERE Title LIKE '%' + @palabraClave + '%'
                                    OR CardSummary LIKE '%' + @palabraClave + '%';
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

this.listSubject = async function () {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT * FROM dbo.Subject;
                        `;
        result = await pool.request().query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.Subject/list", err);
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

this.PostBlog = async function (publishValue,Title, Slug, Tags, PhotoAuthor, SubjectId, PostAuthor, FormPostDate, 
    Judgement, Pleno, Expedient, CardSummary, Body, Imagen, PDFile, Background, ColabId){   
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = ` INSERT INTO dbo.BlogPost(UniqueId,CategoryId, IsPublish, Title, Slug, Tags, PhotoAuthor, SubjectId, PostAuthor, PostDate, Judgement, 
                            Pleno, Expedient, CardSummary, Summary, Auth, Price, Body, Imagen, PDFile, Background, ColabId)
                        VALUES ( NEWID(), 1, @publishValue, @Title, @Slug, @Tags, @PhotoAuthor, @SubjectId, 
                        @PostAuthor, @FormPostDate , @Judgement, @Pleno, @Expedient, @CardSummary, '.', 1, '0.00', @Body, 
                        '/uploads/'+@Imagen,'/uploads/'+@PDFile, '/uploads/'+@Background, @ColabId);
                        `;
                    const request = pool.request();
                    request.input('publishValue', sql.Bit, publishValue);
                    request.input('Title', sql.NVarChar, Title);
                    request.input('Slug', sql.NVarChar, Slug);
                    request.input('Tags', sql.NVarChar, Tags);
                    request.input('PhotoAuthor', sql.NVarChar, PhotoAuthor);
                    request.input('SubjectId', sql.Int, SubjectId);
                    request.input('PostAuthor', sql.NVarChar, PostAuthor);
                    request.input('FormPostDate', sql.DateTime, FormPostDate);
                    request.input('Judgement', sql.NVarChar, Judgement);
                    request.input('Pleno', sql.NVarChar, Pleno);
                    request.input('Expedient', sql.NVarChar, Expedient);
                    request.input('CardSummary', sql.NVarChar, CardSummary);
                    request.input('Body', sql.NText, Body);
                    request.input('Imagen', sql.NVarChar, Imagen);
                    request.input('PDFile', sql.NVarChar, PDFile);
                    request.input('Background', sql.NVarChar, Background);
                    request.input('ColabId', sql.Int, ColabId);
                    result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.BlogPost/list", err);
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

this.blogsDelete = async function (UniqueID) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `
            DELETE FROM dbo.BlogPost
            WHERE UniqueID = @UniqueID;
        `;
        const request = pool.request();
        request.input('UniqueID', sql.UniqueIdentifier, UniqueID);
        result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.BlogPost/delete", err);
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

this.BlogsId = async function (UniqueId) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        const query = `SELECT dbo.BlogPost.UniqueId, IsPublish, Title, Imagen, Body, Summary, Tags, PhotoAuthor, Background, PostAuthor, 
                        CONVERT(varchar, PostDate, 103) AS PostDate,
                        Judgement, Pleno, Expedient, CardSummary, PDFile, ColabId, SubjectId
                        FROM dbo.BlogPost
                        WHERE dbo.BlogPost.UniqueId = @UniqueId;
                        `;        
                const request = pool.request();
                request.input('UniqueId', sql.UniqueIdentifier, UniqueId);
                result = await request.query(query);

    } catch (err) {
        error = err;
        log.logError("dbo.BlogPost/list", err);
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

this.blogsUpdate = async function (updateParams) {
    let pool;
    let result;
    let error = '';
    try {
        pool = await sql.connect(config.db);
        let updateFields = '';

        // Verificacion de campos nulos
        if (updateParams.IsPublish !== undefined){
            updateFields += `IsPublish = 1, `;
        }else{
            updateFields += `IsPublish = 0, `;
        }
        if (updateParams.Imagen !== null){
            updateFields += `Imagen = '/uploads/'+@Imagen, `;
        }
        if (updateParams.Background !== null){
            updateFields += `Background = '/uploads/'+@Background, `;
        }
        if (updateParams.PDFile !== null) {
            updateFields += `PDFile = '/uploads/'+@PDFile, `;
        }

        updateFields = updateFields.trim();
        

        const query = ` UPDATE dbo.BlogPost 
                        SET Title = @Title, Slug = @Slug, Tags = @Tags, 
                        PhotoAuthor = @PhotoAuthor, SubjectId = @SubjectId, PostAuthor = @PostAuthor, 
                        PostDate = @FormPostDate, Judgement = @Judgement, Pleno = @Pleno, Expedient = @Expedient, 
                        CardSummary = @CardSummary, Body = @Body ,
                        ${updateFields} ColabId = @ColabId
                        WHERE UniqueId = @UniqueId; 
                        `;
                    const request = pool.request();
                    request.input('UniqueId', sql.UniqueIdentifier, updateParams.UniqueId);
                    request.input('IsPublish', sql.Bit, updateParams.IsPublish);
                    request.input('Title', sql.NVarChar, updateParams.Title);
                    request.input('Slug', sql.NVarChar, updateParams.Slug);
                    request.input('Tags', sql.NVarChar, updateParams.Tags);
                    request.input('PhotoAuthor', sql.NVarChar, updateParams.PhotoAuthor);
                    request.input('SubjectId', sql.Int, updateParams.SubjectId);
                    request.input('PostAuthor', sql.NVarChar, updateParams.PostAuthor);
                    request.input('FormPostDate', sql.DateTime, updateParams.FormPostDate);
                    request.input('Judgement', sql.NVarChar, updateParams.Judgement);
                    request.input('Pleno', sql.NVarChar, updateParams.Pleno);
                    request.input('Expedient', sql.NVarChar, updateParams.Expedient);
                    request.input('CardSummary', sql.NVarChar, updateParams.CardSummary);
                    request.input('Body', sql.NText, updateParams.Body);
                    request.input('Imagen', sql.NVarChar, updateParams.Imagen);
                    request.input('PDFile', sql.NVarChar, updateParams.PDFile);
                    request.input('Background', sql.NVarChar, updateParams.Background);
                    request.input('ColabId', sql.Int, updateParams.ColabId);
                    result = await request.query(query);
    } catch (err) {
        error = err;
        log.logError("dbo.BlogPost/update", err);
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