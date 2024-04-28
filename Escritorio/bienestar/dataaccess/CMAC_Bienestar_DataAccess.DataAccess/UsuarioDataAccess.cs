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

public class UsuarioDataAccess
{
	private readonly OracleConnection connection;

	public UsuarioDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<UsuarioVM> ObtenerUsuarios()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("CUsuarios", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		List<UsuarioVM> result = SqlMapper.Query<UsuarioVM>((IDbConnection)obj, "TS_TM_Usuario_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public UsuarioVM ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
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
			val.Add("CUsuario", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreUsuarioIn", (object)nombreUsuario, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			UsuarioVM result = SqlMapper.Query<UsuarioVM>((IDbConnection)obj, "TS_TM_Usuario_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarUsuario(UsuarioVM usuario)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("IdUsuarioOut", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("NombreIn", (object)usuario.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("NombreUsuarioIn", (object)usuario.NombreUsuario, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("PasswordIn", (object)(usuario.ActiveDirectory ? null : usuario.Password), (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("ActivedirectoryIn", (object)usuario.ActiveDirectory, (OracleMappingType?)(OracleMappingType)111, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("DniIn", (object)usuario.Dni, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("EmailIn", (object)usuario.Email, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("UsuariocreaIn", (object)usuario.UsuarioCrea, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("employee_idIn", (object)usuario.employee_id, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("IdRolIn", (object)usuario.IdRol, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		int result = SqlMapper.Query<int>((IDbConnection)obj, "TS_TM_Usuario_I01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).First();
		((DbConnection)(object)connection).Close();
		return result;
	}

	public int ActualizarUsuario(UsuarioVM usuario)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("IdUsuarioIn", (object)usuario.IdUsuario, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("NombreIn", (object)usuario.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("PasswordIn", (object)(usuario.ActiveDirectory ? null : usuario.Password), (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("ActivedirectoryIn", (object)usuario.ActiveDirectory, (OracleMappingType?)(OracleMappingType)111, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("DniIn", (object)usuario.Dni, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("EmailIn", (object)usuario.Email, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("UsuarioModiIn", (object)usuario.UsuarioModi, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("IdRolIn", (object)usuario.IdRol, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		SqlMapper.Query((IDbConnection)obj, "TS_TM_Usuario_U01", (object)val, (IDbTransaction)null, true, (int?)30, commandType);
		((DbConnection)(object)connection).Close();
		return 1;
	}

	public int ActualizarEstadoUsuario(int idUsuario, bool estado)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (((DbConnection)(object)connection).State == ConnectionState.Closed)
		{
			((DbConnection)(object)connection).Open();
		}
		OracleDynamicParameters val = new OracleDynamicParameters();
		val.Add("IdUsuarioIn", (object)idUsuario, (OracleMappingType?)(OracleMappingType)113, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		val.Add("EstadoIn", (object)estado, (OracleMappingType?)(OracleMappingType)111, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
		OracleConnection obj = connection;
		CommandType? commandType = CommandType.StoredProcedure;
		SqlMapper.Query((IDbConnection)obj, "TS_TM_Usuario_U02", (object)val, (IDbTransaction)null, true, (int?)30, commandType);
		((DbConnection)(object)connection).Close();
		return 1;
	}
}
