using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Filters;
using CMAC_Bienestar_Core.ViewModels.Reportes;
using CMAC_Bienestar_DataAccess.Configuration;
using Dapper;
using Dapper.Oracle;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace CMAC_Bienestar_DataAccess.DataAccess;

public class ReportesDataAccess
{
	private readonly OracleConnection connection;

	public ReportesDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<ReporteDsctPlanillaVM> GenerarReporteDescuentos(FilterReporte filtro)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtro.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CDescuentos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)filtro.FechaInicio, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IsStockIn", (object)(filtro.isStock ? 4 : 2), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteDsctPlanillaVM> result = SqlMapper.Query<ReporteDsctPlanillaVM>((IDbConnection)obj, "TS_REPORTE_DSCT", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReporteDsctPlanillaUniformeVM> GenerarReporteDescuentosUniformes(FilterReporte filtro)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtro.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CDescuentos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)filtro.FechaInicio, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IsStockIn", (object)(filtro.isStock ? 4 : 2), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteDsctPlanillaUniformeVM> result = SqlMapper.Query<ReporteDsctPlanillaUniformeVM>((IDbConnection)obj, "TS_REPORTE_DSCT_UNIFORMES", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReporteGruposVM> GenerarReporteGrupos(FilterReporte filtro)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtro.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CReporte", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)(filtro.FechaInicio.HasValue ? filtro.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IsStockIn", (object)((!filtro.isStock) ? 1 : 3), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteGruposVM> result = SqlMapper.Query<ReporteGruposVM>((IDbConnection)obj, "TS_REPORTE_GRUPOS", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReporteGruposVM> GenerarReporteGruposCount(FilterReporte filtro)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtro.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CGrupoCount", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)(filtro.FechaInicio.HasValue ? filtro.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IsStockIn", (object)((!filtro.isStock) ? 1 : 3), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteGruposVM> result = SqlMapper.Query<ReporteGruposVM>((IDbConnection)obj, "TS_REPORTE_GRUPOS_COUNT", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReporteUniformesVM> GenerarReporteUniforms(FilterReporte filtroReporteSolicitudes)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtroReporteSolicitudes.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)(filtroReporteSolicitudes.FechaInicio.HasValue ? filtroReporteSolicitudes.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteUniformesVM> result = SqlMapper.Query<ReporteUniformesVM>((IDbConnection)obj, "TS_REPORTE_UNIFORMES", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReporteUniformesVM> GenerarReporteUniformsStock(FilterReporte filtroReporteSolicitudes)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtroReporteSolicitudes.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)(filtroReporteSolicitudes.FechaInicio.HasValue ? filtroReporteSolicitudes.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteUniformesVM> result = SqlMapper.Query<ReporteUniformesVM>((IDbConnection)obj, "TS_REPORTE_UNIFORMES_STOCK", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<ReportePlantillaSolicitantesVM> ObtenerUsuariosReportePlantillaSolicitantes(FilterReporteSolicitudesCM filtroReporte, bool esStock)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		DateTime? dateTime = filtroReporte.FechaFin;
		dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("CUsuarios", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("FechaInicioIn", (object)(filtroReporte.FechaInicio.HasValue ? filtroReporte.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("EstadoUsuarioIn", (object)((!filtroReporte.EstadoUsuario.HasValue) ? (-1) : (filtroReporte.EstadoUsuario.Value ? 1 : 0)), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("TipoRegularIn", (object)((!esStock) ? TipoPedidoEnum.Regular : TipoPedidoEnum.KardexRegular), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		List<ReportePlantillaSolicitantesVM> result = SqlMapper.Query<ReportePlantillaSolicitantesVM>((IDbConnection)obj, "TS_TM_REP_PLAN_SOLI", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public ICollection<ReporteSolicitudesVM> ObtenerUsuariosReporteSolicitudes(FilterReporteSolicitudesCM filtroReporte, bool esStock)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		DateTime? dateTime = filtroReporte.FechaFin;
		dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("CUsuarios", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("FechaInicioIn", (object)(filtroReporte.FechaInicio.HasValue ? filtroReporte.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("FechaFinIn", (object)dateTime, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("EstadoUsuarioIn", (object)((!filtroReporte.EstadoUsuario.HasValue) ? (-1) : (filtroReporte.EstadoUsuario.Value ? 1 : 0)), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("TipoRegularIn", (object)((!esStock) ? TipoPedidoEnum.Regular : TipoPedidoEnum.KardexRegular), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("TipoPorDescuentoIn", (object)(esStock ? TipoPedidoEnum.KardexDescuento : TipoPedidoEnum.Descuento), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		List<ReporteSolicitudesVM> result = SqlMapper.Query<ReporteSolicitudesVM>((IDbConnection)obj, "TS_TM_REP_SOLI", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public ICollection<ReporteRanzaVM> GenerarReporteRanza(FilterReporte filtro)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			DateTime? dateTime = filtro.FechaFin;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CReporte", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaInicioIn", (object)(filtro.FechaInicio.HasValue ? filtro.FechaInicio : new DateTime?(new DateTime(2000, 1, 1))), (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("FechaFinIn", (object)filtro.FechaFin, (OracleMappingType?)(OracleMappingType)106, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IsStockIn", (object)((!filtro.isStock) ? 1 : 3), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<ReporteRanzaVM> result = SqlMapper.Query<ReporteRanzaVM>((IDbConnection)obj, "TS_REPORTE_RANZA", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}
}
