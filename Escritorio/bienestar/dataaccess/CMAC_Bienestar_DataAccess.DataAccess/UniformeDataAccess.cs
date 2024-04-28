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

public class UniformeDataAccess
{
	private readonly OracleConnection connection;

	public UniformeDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}

	public ICollection<UniformeVM> ObtenerUniformes()
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
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<UniformeVM, TallaVM, UniformeVM> obj2 = delegate(UniformeVM uniforme, TallaVM talla)
			{
				uniforme.Tallas.Add(talla);
				return uniforme;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			IEnumerable<UniformeVM> source = SqlMapper.Query<UniformeVM, TallaVM, UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q01", obj2, (object)val, (IDbTransaction)null, true, "IdTalla", (int?)30, commandType);
			List<UniformeVM> result = (from u in source
				group u by u.IdUniforme).Select(delegate(IGrouping<int, UniformeVM> t)
			{
				UniformeVM uniformeVM = t.First();
				uniformeVM.Tallas = t.Select((UniformeVM u) => u.Tallas.Single()).ToList();
				uniformeVM.TallaEstandar = new TallaVM
				{
					IdTalla = uniformeVM.IdTallaEstandar,
					NombreTalla = uniformeVM.NombreTallaEstandar
				};
				return uniformeVM;
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

	public UniformeVM? ObtenerUniformePorId(int idUniforme)
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
			val.Add("CUniforme", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<UniformeVM, TallaVM, UniformeVM> obj2 = delegate(UniformeVM uniforme, TallaVM talla)
			{
				uniforme.Tallas.Add(talla);
				return uniforme;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			IEnumerable<UniformeVM> source = SqlMapper.Query<UniformeVM, TallaVM, UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q02", obj2, (object)val, (IDbTransaction)null, true, "IdTalla", (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return (from u in source
				group u by u.IdUniforme).Select(delegate(IGrouping<int, UniformeVM> t)
			{
				UniformeVM uniformeVM = t.First();
				uniformeVM.Tallas = t.Select((UniformeVM u) => u.Tallas.Single()).ToList();
				uniformeVM.TallaEstandar = new TallaVM
				{
					IdTalla = uniformeVM.IdTallaEstandar,
					NombreTalla = uniformeVM.NombreTallaEstandar
				};
				return uniformeVM;
			}).FirstOrDefault();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public UniformeVM? ObtenerUniformePorIdPedido(int idUniforme)
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
			val.Add("CUniforme", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			UniformeVM result = SqlMapper.Query<UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q06", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public List<UniformeVM> ObtenerUniformesPorGrupo(string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, string sexo)
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
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoRegionIn", (object)codigoRegion, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoSedeIn", (object)codigoSede, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoUnidadIn", (object)codigoUnidad, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("CodigoPuestoIn", (object)codigoPuesto, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("SexoIn", (object)sexo, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<UniformeVM, TallaVM, UniformeVM> obj2 = delegate(UniformeVM uniforme, TallaVM talla)
			{
				uniforme.Tallas.Add(talla);
				return uniforme;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			List<UniformeVM> source = SqlMapper.Query<UniformeVM, TallaVM, UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q03", obj2, (object)val, (IDbTransaction)null, true, "IdTalla", (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			IEnumerable<UniformeVM> source2 = (from u in source
				group u by u.IdUniforme).Select(delegate(IGrouping<int, UniformeVM> t)
			{
				UniformeVM uniformeVM = t.First();
				uniformeVM.Tallas = t.Select((UniformeVM u) => u.Tallas.Single()).ToList();
				return uniformeVM;
			});
			return source2.ToList();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int AgregarUniforme(UniformeVM uniforme)
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
			val.Add("NombreIn", (object)uniforme.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("SKUIn", (object)uniforme.SKU, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GeneroIn", (object)uniforme.Genero, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdTallaEstandarIn", (object)uniforme.IdTallaEstandar, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GUIDImagenIn", (object)uniforme.GUIDImagen, (OracleMappingType?)(OracleMappingType)105, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("DetalleIn", (object)uniforme.Detalle, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GUIDDetalleIn", (object)uniforme.GUIDDetalle, (OracleMappingType?)(OracleMappingType)105, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("PrecioIn", (object)uniforme.Precio, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("UsuarioCreaIn", (object)"admin", (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TM_Uniforme_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdUniformeOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int ActualizarUniforme(UniformeVM uniforme)
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
			val.Add("IdUniformeIn", (object)uniforme.IdUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("NombreIn", (object)uniforme.Nombre, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("SKUIn", (object)uniforme.SKU, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GeneroIn", (object)uniforme.Genero, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdTallaEstandarIn", (object)uniforme.IdTallaEstandar, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GUIDImagenIn", (object)uniforme.GUIDImagen, (OracleMappingType?)(OracleMappingType)105, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("DetalleIn", (object)uniforme.Detalle, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("GUIDDetalleIn", (object)uniforme.GUIDDetalle, (OracleMappingType?)(OracleMappingType)105, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("PrecioIn", (object)uniforme.Precio, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("EstadoIn", (object)(uniforme.Estado ? 1 : 0), (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("UsuarioModiIn", (object)"admin", (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TM_Uniforme_U01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdUniformeOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public int EliminarUniforme(int idUniforme)
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
			SqlMapper.Execute((IDbConnection)obj, "TS_TM_Uniforme_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
			return val.Get<int>("IdUniformeOut");
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public List<UniformeVM> ObtenerUniformesPorGrupo(int idGrupo)
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
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			Func<UniformeVM, TallaVM, UniformeVM> obj2 = delegate(UniformeVM uniforme, TallaVM talla)
			{
				uniforme.Tallas.Add(talla);
				return uniforme;
			};
			CommandType? commandType = CommandType.StoredProcedure;
			List<UniformeVM> source = SqlMapper.Query<UniformeVM, TallaVM, UniformeVM>((IDbConnection)obj, "TS_TR_Uniforme_Grupo_Q01", obj2, (object)val, (IDbTransaction)null, true, "IdTalla", (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			IEnumerable<UniformeVM> source2 = (from u in source
				group u by u.IdUniforme).Select(delegate(IGrouping<int, UniformeVM> t)
			{
				UniformeVM uniformeVM = t.First();
				uniformeVM.Tallas = t.Select((UniformeVM u) => u.Tallas.Single()).ToList();
				uniformeVM.TallaEstandar = new TallaVM
				{
					IdTalla = uniformeVM.IdTallaEstandar,
					NombreTalla = uniformeVM.NombreTallaEstandar
				};
				return uniformeVM;
			});
			return source2.ToList();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public void AgregarUniformesPorGrupo(int idUniforme, int idGrupo, int item)
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
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdGrupoIn", (object)idGrupo, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("ItemIn", (object)item, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Uniforme_Grupo_I01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public void ActualizarUniformesPorGrupo(int idUniforme, string usuario, int item)
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
			val.Add("IdUniformeGrupoOut", (object)null, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("IdUniformeIn", (object)idUniforme, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("Usuario", (object)usuario, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("ItemIn", (object)item, (OracleMappingType?)(OracleMappingType)112, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			SqlMapper.Execute((IDbConnection)obj, "TS_TR_Uniforme_Grupo_U01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public void EliminarUniformesPorGrupo(int idGrupo)
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
			int num = SqlMapper.Execute((IDbConnection)obj, "TS_TR_Uniforme_Grupo_D01", (object)val, (IDbTransaction)null, (int?)30, commandType);
			((DbConnection)(object)connection).Close();
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public ICollection<UniformeVM> ObtenerUniformesSimple()
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
			val.Add("CUniformes", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			List<UniformeVM> result = SqlMapper.Query<UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q04", (object)val, (IDbTransaction)null, true, (int?)30, commandType).ToList();
			((DbConnection)(object)connection).Close();
			return result;
		}
		catch (Exception ex)
		{
			((DbConnection)(object)connection).Close();
			throw new Exception(ex.Message, ex);
		}
	}

	public UniformeVM? ObtenerUniformePorNombre(string Uniforme_name)
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
			val.Add("CUniforme", (object)null, (OracleMappingType?)(OracleMappingType)121, (ParameterDirection?)ParameterDirection.Output, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			val.Add("UniformeNameIn", (object)Uniforme_name, (OracleMappingType?)(OracleMappingType)126, (ParameterDirection?)ParameterDirection.Input, (int?)null, (bool?)null, (byte?)null, (byte?)null, (string)null, (DataRowVersion?)null, (OracleMappingCollectionType?)null, (int[])null);
			OracleConnection obj = connection;
			CommandType? commandType = CommandType.StoredProcedure;
			UniformeVM result = SqlMapper.Query<UniformeVM>((IDbConnection)obj, "TS_TM_Uniforme_Q05", (object)val, (IDbTransaction)null, true, (int?)30, commandType).FirstOrDefault();
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
