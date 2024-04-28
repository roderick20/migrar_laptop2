const log = require('../utils/log.js');
const sql = require('mssql');
const config = require('../utils/config.js');

module.exports = function (app) {

    this.create = async function (params) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = ` INSERT INTO dbo.Teacher
                        (Name, LastName, TypeDoc, NumDoc, Email, Password, Status, Created, LastUpdate)
                        VALUES (@Name, @LastName, @TypeDoc, @NumDoc, @Email, CONVERT(NVARCHAR(32), HashBytes('MD5', '${params.Password}'), 2), 1, getdate(), getdate());`;
            const request = pool.request();
            request.input('Name', sql.NVarChar, params.Name);
            request.input('LastName', sql.NVarChar, params.LastName);
            request.input('TypeDoc', sql.NVarChar, params.TypeDoc);
            request.input('NumDoc', sql.NVarChar, params.NumDoc);
            request.input('Email', sql.NVarChar, params.Email);
            request.input('Password', sql.NVarChar, params.Password);

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


    this.findAllId = async function (userid) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `SELECT * FROM [dbo].[Teacher] WHERE Id = @userid`;
            const request = pool.request();
            request.input('userid', sql.BigInt, userid);
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

    this.update = async function (Id, params) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);


            const query = `
                UPDATE dbo.Teacher
                SET
                    Name = @Name,
                    LastName = @LastName,
                    Country = @Country,
                    CountryResidence = @CountryResidence,
                    TypeDoc = @TypeDoc,
                    NumDoc = @NumDoc,
                    Email = @Email,
                    Birthdate = @Birthdate,
                    Phone = @Phone,
                    LinkedIn = @LinkedIn,

                    FechaIngreso = @FechaIngreso,
                    DocenteLey30220 = @DocenteLey30220,
                    IncorpDespLicencia = @IncorpDespLicencia,
                    GradoAcademico = @GradoAcademico,
                    MencionGrado = @MencionGrado,
                    PaisUniGrado = @PaisUniGrado,
                    UniGrado = @UniGrado,

                    NivelMaestria = @NivelMaestria,
                    NivelDoctorado = @NivelDoctorado,
                    RolPresencial = @RolPresencial,
                    SemiDeterminar = @SemiDeterminar,
                    SemiAdaptar = @SemiAdaptar,
                    SemiImpartir = @SemiImpartir,
                    Tutoria = @Tutoria,
                    CategoriaDocente = @CategoriaDocente,
                    AniosCategoria = @AniosCategoria,
                    RegimenDedicacion = @RegimenDedicacion,
                    HorasSemanalesClases = @HorasSemanalesClases,
                    HorasSemanalesOtrasActividades = @HorasSemanalesOtrasActividades,
                    TotalHorasSemanales = @TotalHorasSemanales,
                    AniosExperienciaSemipresencial = @AniosExperienciaSemipresencial,
                    HorasFormacionVirtual = @HorasFormacionVirtual,

                    RealizaInvestigacion = @RealizaInvestigacion,
                    RegistradoDINA = @RegistradoDINA,
                    LinkDINA = @LinkDINA,
                    RegistradoRENACYT = @RegistradoRENACYT,
                    FechaRegistroRENACYT = @FechaRegistroRENACYT,
                    LinkRENACYT = @LinkRENACYT,
                    CodigoRENACYT =@CodigoRENACYT,
                    FechaInicioContrato = @FechaInicioContrato,
                    FechaFinContrato = @FechaFinContrato,
                    InformacionActualizada = @InformacionActualizada,
                    [LastUpdate] = getdate()

                    WHERE Id = @Id;
                    `;

                    
                   
               
            const request = pool.request();
            request.input('Id', sql.Int, Id);
            request.input('Name', sql.NVarChar, params.Name);
            request.input('LastName', sql.NVarChar, params.LastName);
            request.input('Country', sql.NVarChar, params.Country);
            request.input('CountryResidence', sql.NVarChar, params.CountryResidence);
            request.input('TypeDoc', sql.NVarChar, params.TypeDoc);
            request.input('NumDoc', sql.NVarChar, params.NumDoc);
            request.input('Email', sql.NVarChar, params.Email);
            request.input('Birthdate', sql.NVarChar, params.Birthdate);
            request.input('Phone', sql.NVarChar, params.Phone);
            request.input('LinkedIn', sql.NVarChar, params.LinkedIn);

            request.input('FechaIngreso', sql.NVarChar, params.FechaIngreso);
            request.input('DocenteLey30220', sql.NVarChar, params.DocenteLey30220);
            request.input('IncorpDespLicencia', sql.NVarChar, params.IncorpDespLicencia);
            request.input('GradoAcademico', sql.NVarChar, params.GradoAcademico);
            request.input('MencionGrado', sql.NVarChar, params.MencionGrado);
            request.input('UniGrado', sql.NVarChar, params.UniGrado);
            request.input('PaisUniGrado', sql.NVarChar, params.PaisUniGrado);

            request.input('NivelMaestria', sql.NVarChar, params.NivelMaestria);
            request.input('NivelDoctorado', sql.NVarChar, params.NivelDoctorado);
            request.input('RolPresencial', sql.NVarChar, params.RolPresencial);
            request.input('SemiDeterminar', sql.NVarChar, params.SemiDeterminar);
            request.input('SemiAdaptar', sql.NVarChar, params.SemiAdaptar);
            request.input('SemiImpartir', sql.NVarChar, params.SemiImpartir);
            request.input('Tutoria', sql.NVarChar, params.Tutoria);
            request.input('CategoriaDocente', sql.NVarChar, params.CategoriaDocente);
            request.input('AniosCategoria', sql.NVarChar, params.AniosCategoria);
            request.input('RegimenDedicacion', sql.NVarChar, params.RegimenDedicacion);
            request.input('HorasSemanalesClases', sql.NVarChar, params.HorasSemanalesClases);
            request.input('HorasSemanalesOtrasActividades', sql.NVarChar, params.HorasSemanalesOtrasActividades);
            request.input('TotalHorasSemanales', sql.NVarChar, params.TotalHorasSemanales);
            request.input('AniosExperienciaSemipresencial', sql.NVarChar, params.AniosExperienciaSemipresencial);
            request.input('HorasFormacionVirtual', sql.NVarChar, params.HorasFormacionVirtual);

            request.input('RealizaInvestigacion', sql.NVarChar, params.RealizaInvestigacion);
            request.input('RegistradoDINA', sql.NVarChar, params.RegistradoDINA);
            request.input('LinkDINA', sql.NVarChar, params.LinkDINA);
            
            request.input('RegistradoRENACYT', sql.NVarChar, params.RegistradoRENACYT);
            request.input('FechaRegistroRENACYT', sql.NVarChar, params.FechaRegistroRENACYT);
            request.input('LinkRENACYT', sql.NVarChar, params.LinkRENACYT);
            request.input('CodigoRENACYT', sql.NVarChar, params.CodigoRENACYT);
            request.input('FechaInicioContrato', sql.NVarChar, params.FechaInicioContrato);
            request.input('FechaFinContrato', sql.NVarChar, params.FechaFinContrato);
            request.input('InformacionActualizada', sql.NVarChar, params.InformacionActualizada);
            


           
            
            
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Teacher/update", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }

    this.updatecv = async function (Id, filename) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
                UPDATE dbo.Teacher
                SET
                    RutaCV = @RutaCV
                WHERE Id = @Id;
            `;
            const request = pool.request();
            request.input('Id', sql.Int, Id);
            request.input('RutaCV', sql.NVarChar, filename);
            
            
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Teacher/update", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }

    this.updateFoto = async function (id, RutaFoto) {
        let pool, result, error = '';
        try {
            pool = await sql.connect(config.db);
            const query = `
                UPDATE dbo.Teacher
                SET
                RutaFoto = @RutaFoto WHERE id = @id;
            `;
            const request = pool.request();            
            request.input('RutaFoto', sql.NVarChar, RutaFoto);
            request.input('id', sql.Int, id);
            result = await request.query(query);
        } catch (err) {
            error = err;
            log.logError("dbo.Teacher/update", err);
        } finally {
            if (pool) {
                pool.close();
            }
        }
        return { result, error }
    }
    
    


  
    return this;

}