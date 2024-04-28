using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.Configuration;
using Dapper;
using Dapper.Oracle;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace CMAC_Bienestar_DataAccess.DataAccess;

public class GrupoDataAccess
{
	private readonly OracleConnection connection;

	private readonly UniformeDataAccess uniformeDataAccess;

	public GrupoDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
		uniformeDataAccess = new UniformeDataAccess(configuration);
	}

	public ICollection<GrupoVM> ObtenerGrupos()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CGrupos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<GrupoVM, GrupoEntidadVM, GrupoVM> obj2 = delegate(GrupoVM grupo, GrupoEntidadVM entidad)
			{
				grupo.GrupoEntidades.Add(entidad);
				return grupo;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			List<GrupoVM> source = SqlMapper.Query<GrupoVM, GrupoEntidadVM, GrupoVM>((IDbConnection)obj, "TS_TM_Grupo_Q01", obj2, (object)val, (IDbTransaction)null, true, "IdGrupoEntidad", (int?)60, commandType).ToList();
			List<GrupoVM> result = (from u in source
				group u by u.IdGrupo).Select(delegate(IGrouping<int, GrupoVM> t)
			{
				GrupoVM grupoVM = t.First();
				grupoVM.GrupoEntidades = t.Select((GrupoVM u) => u.GrupoEntidades.Single()).ToList();
				return grupoVM;
			}).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public GrupoVM? ObtenerGrupoPorId(int idGrupo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CGrupo", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<GrupoVM, GrupoEntidadVM, GrupoVM> obj2 = delegate(GrupoVM grupo, GrupoEntidadVM entidad)
			{
				grupo.GrupoEntidades.Add(entidad);
				return grupo;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			List<GrupoVM> source = SqlMapper.Query<GrupoVM, GrupoEntidadVM, GrupoVM>((IDbConnection)obj, "TS_TM_Grupo_Q02", obj2, (object)val, (IDbTransaction)null, true, "IdGrupoEntidad", (int?)60, commandType).ToList();
			GrupoVM result = (from u in source
				group u by u.IdGrupo).Select(delegate(IGrouping<int, GrupoVM> t)
			{
				GrupoVM grupoVM = t.First();
				grupoVM.GrupoEntidades = t.Select((GrupoVM u) => u.GrupoEntidades.Single()).ToList();
				grupoVM.Uniformes = uniformeDataAccess.ObtenerUniformesPorGrupo(grupoVM.IdGrupo).ToList();
				return grupoVM;
			}).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}

	public GrupoVM? ObtenerGrupoPorNombre(string nombre)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CGrupo", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreIn", (object)nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<GrupoVM, GrupoEntidadVM, GrupoVM> obj2 = delegate(GrupoVM grupo, GrupoEntidadVM entidad)
			{
				grupo.GrupoEntidades.Add(entidad);
				return grupo;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			List<GrupoVM> source = SqlMapper.Query<GrupoVM, GrupoEntidadVM, GrupoVM>((IDbConnection)obj, "TS_TM_Grupo_Q04", obj2, (object)val, (IDbTransaction)null, true, "IdGrupoEntidad", (int?)60, commandType).ToList();
			GrupoVM result = (from u in source
				group u by u.IdGrupo).Select(delegate(IGrouping<int, GrupoVM> t)
			{
				GrupoVM grupoVM = t.First();
				grupoVM.GrupoEntidades = t.Select((GrupoVM u) => u.GrupoEntidades.Single()).ToList();
				return grupoVM;
			}).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarGrupo(GrupoVM grupo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreIn", (object)grupo.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GeneroIn", (object)grupo.Genero, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("UsuarioCreaIn", (object)"admin", (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TM_Grupo_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int ActualizarGrupo(GrupoVM grupo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)grupo.IdGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreIn", (object)grupo.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GeneroIn", (object)grupo.Genero, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("EstadoIn", (object)(grupo.Estado ? 1 : 0), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("UsuarioModiIn", (object)grupo.UsuarioModi, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TM_Grupo_U01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarGrupo(int idGrupo)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		try
		{
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TM_Grupo_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarTotalGrupo(int idGrupo)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		try
		{
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TM_Grupo_D02", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public GrupoVM? ObtenerGrupoPorFiltros(string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, string sexo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("CGrupo", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoRegionIn", (object)codigoRegion, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoSedeIn", (object)codigoSede, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoUnidadIn", (object)codigoUnidad, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoPuestoIn", (object)codigoPuesto, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("SexoIn", (object)sexo, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			GrupoVM result = SqlMapper.Query<GrupoVM>((IDbConnection)obj, "TS_TM_Grupo_Q03", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarGrupoEntidad(GrupoEntidadVM grupo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoEntidadOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoRegionIn", (object)grupo.CodigoRegion, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoSedeIn", (object)grupo.CodigoSede, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoUnidadIn", (object)grupo.CodigoUnidad, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoPuestoIn", (object)grupo.CodigoPuesto, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)grupo.IdGrupo, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TR_Grupo_Entidad_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoEntidadOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarGrupoEntidad(int idGrupo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleDynamicParameters val = new OracleDynamicParameters();
			val.Add("IdGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TR_Grupo_Entidad_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}
}
