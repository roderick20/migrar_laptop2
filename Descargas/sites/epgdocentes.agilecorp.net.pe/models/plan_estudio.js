const log = require('../utils/log.js');
const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.findPlanEstudio = async function () {
        try {
            await sql.connect(config.db);
            const query = `SELECT [PlanEstudio] FROM [dbo].[PlanEstudio] group by [PlanEstudio]`;
            var request = new sql.Request();
            result = await request.query(query);
            await sql.close();
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.findGrado = async function (params) {
        try {
            await sql.connect(config.db);
            const query = `SELECT [PlanEstudio], [Grado] FROM [dbo].[PlanEstudio] WHERE  [PlanEstudio] = '${params.PlanEstudio}' group by [PlanEstudio], [Grado] `;
            var request = new sql.Request();
            result = await request.query(query);
            await sql.close();
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.findModalidad = async function (params) {
        try {
            await sql.connect(config.db);
            const query = `SELECT [PlanEstudio], [Grado], [Modalidad] FROM [dbo].[PlanEstudio] WHERE  [PlanEstudio] = '${params.PlanEstudio}' AND [Grado] = '${params.Grado}' group by [PlanEstudio], [Grado], [Modalidad]  `;
            var request = new sql.Request();
            result = await request.query(query);
            await sql.close();
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.findPrograma = async function (params) {
        try {
            await sql.connect(config.db);
            const query = `SELECT [PlanEstudio], [Grado], [Modalidad], [Programa] FROM [dbo].[PlanEstudio] WHERE  [PlanEstudio] = '${params.PlanEstudio}' AND [Grado] = '${params.Grado}'  AND [Modalidad] = '${params.Modalidad}' group by [PlanEstudio], [Grado], [Modalidad], [Programa] `;
            var request = new sql.Request();
            result = await request.query(query);
            await sql.close();
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }

    this.findAsignatura = async function (params) {
        try {
            await sql.connect(config.db);
            const query = `SELECT Id, [PlanEstudio], [Grado], [Modalidad], [Programa], [Asignatura] FROM [dbo].[PlanEstudio] WHERE  [PlanEstudio] = '${params.PlanEstudio}' AND [Grado] = '${params.Grado}'  AND [Modalidad] = '${params.Modalidad}' AND [Programa] = '${params.Programa}' group by Id, [PlanEstudio], [Grado], [Modalidad], [Programa], [Asignatura] `;
            var request = new sql.Request();
            result = await request.query(query);
            await sql.close();
            return result;
        } catch (err) {
            console.error('Error al conectar a la base de datos', err);
        }
    }
    
  


 

    
    return this;
}