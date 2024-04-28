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

public class RolDataAccess
{
	private readonly OracleConnection connection;

	public RolDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<RolVM> ObtenerRoles()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("CRol", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		List<RolVM> result = SqlMapper.Query<RolVM>((IDbConnection)obj, "TS_TM_Rol_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public RolVM ObtenerRolPorNombre(string nombre)
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
			val.Add("CRol", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreIn", (object)nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			RolVM result = SqlMapper.Query<RolVM>((IDbConnection)obj, "TS_TM_Rol_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarRol(RolVM rol)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("IdRolOut", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("Nombre1", (object)rol.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("UsuarioCrea1", (object)rol.UsuarioCrea, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		int result = SqlMapper.Query<int>((IDbConnection)obj, "TS_TM_Rol_I01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).First();
		((DbConnection)(object)connection).Close();
		return result;
	}
}
