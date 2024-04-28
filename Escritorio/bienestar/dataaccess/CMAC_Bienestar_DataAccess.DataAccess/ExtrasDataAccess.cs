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

public class ExtrasDataAccess
{
	private readonly OracleConnection connection;

	public ExtrasDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<SedeVM> ObtenerSedes()
	{
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.Text;
			List<SedeVM> result = SqlMapper.Query<SedeVM>((IDbConnection)obj, "SELECT CO_SEDE AS CodigoSede, DE_SEDE DetalleSede FROM TTSEDE@OFIP", (object)null, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<SedeVM> ObtenerSedesPorGrupo(int idGrupo)
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
			val.Add("CSedes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<SedeVM> result = SqlMapper.Query<SedeVM>((IDbConnection)obj, "TS_TR_Sede_Grupo_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<SedeVM> ObtenerTodasSedesConGrupo()
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
			val.Add("CSedes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<SedeVM> result = SqlMapper.Query<SedeVM>((IDbConnection)obj, "TS_TR_Sede_Grupo_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarSedePorGrupo(string codigoSede, int idGrupo)
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
			val.Add("IdSedeGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoSedeIn", (object)codigoSede, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Sede_Grupo_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdSedeGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarSedePorGrupo(int idGrupo)
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
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Sede_Grupo_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<UnidadVM> ObtenerUnidades()
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
			val.Add("CUnidades", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.Text;
			List<UnidadVM> result = SqlMapper.Query<UnidadVM>((IDbConnection)obj, "SELECT CO_UNID AS CodigoUnidad, DE_UNID AS DetalleUnidad FROM TMUNID_EMPR@OFIP", (object)null, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<UnidadVM> ObtenerUnidadesPorGrupo(int idGrupo)
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
			val.Add("CUnidades", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<UnidadVM> result = SqlMapper.Query<UnidadVM>((IDbConnection)obj, "TS_TR_Unidad_Grupo_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<UnidadVM> ObtenerTodasUnidadesConGrupo()
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
			val.Add("CUnidades", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<UnidadVM> result = SqlMapper.Query<UnidadVM>((IDbConnection)obj, "TS_TR_Unidad_Grupo_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarUnidadPorGrupo(string codigoUnidad, int idGrupo)
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
			val.Add("IdUnidadGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoUnidadIn", (object)codigoUnidad, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Unidad_Grupo_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdUnidadGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarUnidadPorGrupo(int idGrupo)
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
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Unidad_Grupo_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<PuestoVM> ObtenerPuestos()
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
			val.Add("CPuestos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.Text;
			List<PuestoVM> result = SqlMapper.Query<PuestoVM>((IDbConnection)obj, "SELECT DISTINCT \"cargo_empl\" AS CodigoPuesto, \"cargo_empl\" AS DetallePuesto FROM DATOS_EMPL@OFIP", (object)null, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<PuestoVM> ObtenerPuestosPorGrupo(int idGrupo)
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
			val.Add("CPuestos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<PuestoVM> result = SqlMapper.Query<PuestoVM>((IDbConnection)obj, "TS_TR_Puesto_Grupo_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<PuestoVM> ObtenerTodasPuestosConGrupo()
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
			val.Add("CPuestos", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<PuestoVM> result = SqlMapper.Query<PuestoVM>((IDbConnection)obj, "TS_TR_Puesto_Grupo_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarPuestoPorGrupo(string codigoPuesto, int idGrupo)
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
			val.Add("IdPuestoGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoPuestoIn", (object)codigoPuesto, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Puesto_Grupo_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdPuestoGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarPuestoPorGrupo(int idGrupo)
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
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Puesto_Grupo_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdGrupoOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<TrabajadorVM> ObtenerTrabajadores()
	{
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.Text;
			List<TrabajadorVM> result = SqlMapper.Query<TrabajadorVM>((IDbConnection)obj, "SELECT \"employee_id\" employee_id, \"employee_num\" DNI, \"usuario_empl\" NombreUsuario, \"full_name\" Nombre, \"st_sexo\" Sexo, \"fe_ingr_corp\" FechaIngresoCorporacion, \"cargo_empl\" CodigoPuesto, \"cargo_empl\" Puesto, \"co_unid\" CodigoUnidad, \"unid_empl\" Unidad, \"co_sede\" CodigoSede, \"sede_empl\" Sede FROM DATOS_EMPL@OFIP", (object)null, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<TrabajadorVM> ObtenerTrabajadoresYColaboradoresSinUsuarios()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("CTrabajadores", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		List<TrabajadorVM> result = SqlMapper.Query<TrabajadorVM>((IDbConnection)obj, "TS_TM_Trabajador_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public TrabajadorVM ObtenerTrabajadorPorNombreUsuario(string nombreUsuario)
	{
		string nombreUsuario2 = nombreUsuario;
		try
		{
			if (((DbConnection)(object)connection).State == ConnectionState.Closed)
			{
				((DbConnection)(object)connection).Open();
			}
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.Text;
			List<TrabajadorVM> source = SqlMapper.Query<TrabajadorVM>((IDbConnection)obj, "SELECT \"employee_id\" employee_id, \"employee_num\" DNI, \"usuario_empl\" NombreUsuario, \"full_name\" Nombre, \"st_sexo\" Sexo, \"fe_ingr_corp\" FechaIngresoCorporacion, \"co_pues_trab\" CodigoPuesto, \"cargo_empl\" Puesto, \"co_unid\" CodigoUnidad, \"unid_empl\" Unidad, \"co_sede\" CodigoSede, \"sede_empl\" Sede FROM DATOS_EMPL@OFIP", (object)null, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			TrabajadorVM result = source.Where((TrabajadorVM x) => x.NombreUsuario == nombreUsuario2).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}
}
