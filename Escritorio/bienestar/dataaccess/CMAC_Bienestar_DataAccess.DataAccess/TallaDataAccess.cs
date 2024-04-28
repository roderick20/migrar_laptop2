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

public class TallaDataAccess
{
	private readonly OracleConnection connection;

	public TallaDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<TallaVM> ObtenerTodasTallas()
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
			val.Add("CTallas", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<TallaVM> result = SqlMapper.Query<TallaVM>((IDbConnection)obj, "TS_TM_Talla_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public TallaVM? ObtenerTallaPorId(int idTalla)
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
			val.Add("CTalla", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdTallaIn", (object)idTalla, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			TallaVM result = SqlMapper.Query<TallaVM>((IDbConnection)obj, "TS_TM_Talla_Q02", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<TallaVM> ObtenerTallasPorUniforme(int idUniforme)
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
			val.Add("CTallas", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<TallaVM> result = SqlMapper.Query<TallaVM>((IDbConnection)obj, "TS_TR_Talla_Uniforme_Q01", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public void AgregarTallasPorUniforme(int idTalla, int idUniforme)
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
			val.Add("IdTallaUniformeOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdTallaIn", (object)idTalla, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Talla_Uniforme_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public void EliminarTallasPorUniforme(int idUniforme)
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
			val.Add("IdUniformeOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Talla_Uniforme_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}
}
