const log = require('../utils/log.js');
const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.create = async function (params,TeacherId) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `INSERT INTO [dbo].[TeacherPlanEstudio]
                        ([PlanEstudioId] ,[TeacherId])
                        VALUES (@PlanEstudioId, @TeacherId);`;
            const request = pool.request();
            request.input('PlanEstudioId', sql.NVarChar, params.PlanEstudioId);
            request.input('TeacherId', sql.NVarChar, TeacherId);
           

            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Teacher/create", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }


    this.findByTeacherId = async function (userid) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT pe.[Id]
            ,pe.[PlanEstudio]
            ,pe.[Grado]
            ,pe.[Modalidad]
            ,pe.[Programa]
            ,pe.[Asignatura]
            ,tpe.Id as PlanEstudioId
        FROM [dbo].[PlanEstudio] pe
        LEFT JOIN TeacherPlanEstudio tpe ON tpe.[PlanEstudioId] = pe.Id
        WHERE tpe.[TeacherId] = @userid`;
            const request = pool.request();
            request.input('userid', sql.Int, userid);
            result = await request.query(query);

        } catch (err) {
            error = err;
            log.logError("teacher/findAll", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }

    this.delete = async function (id) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `DELETE FROM TeacherPlanEstudio WHERE Id = @id`;
            const request = pool.request();
            request.input('id', sql.Int, id);
            result = await request.query(query);

        } catch (err) {
            error = err;
            log.logError("teacher/findAll", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }
    


  
    return this;

}