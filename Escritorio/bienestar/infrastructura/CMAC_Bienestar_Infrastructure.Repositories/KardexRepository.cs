using System.Collections.Generic;
using System.Linq;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Filters;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class KardexRepository : IKardexRepository
{
	private readonly KardexDataAccess kardexDataAccess;

	public KardexRepository(IConfiguration configuration)
	{
		kardexDataAccess = new KardexDataAccess(configuration);
	}

	public void ActualizarKardex(KardexVM kardex)
	{
		kardexDataAccess.ActualizarKardex(kardex);
	}

	public int AgregarKardex(KardexVM periodo)
	{
		return kardexDataAccess.AgregarKardex(periodo);
	}

	public void EliminarKardex(int idKardex)
	{
		kardexDataAccess.EliminarKardex(idKardex);
	}

	public KardexVM ObtenerKardexPorId(int idKardex)
	{
		return kardexDataAccess.ObtenerKardexPorId(idKardex);
	}

	public ICollection<KardexVM> ObtenerKardexPorUniformeTalla(FilterKardexVM filtro)
	{
		int value = 0;
		ICollection<KardexVM> collection = kardexDataAccess.ObtenerKardexPorUniformeTalla(filtro);
		foreach (KardexVM item in collection.Reverse())
		{
			item.CantidadInicial = value;
			if (item.TipoRegistro == TipoRegistroKardexEnum.Entrada)
			{
				item.CantidadFinal = item.CantidadInicial + item.Valor;
			}
			else
			{
				item.CantidadFinal = item.CantidadInicial - item.Valor;
			}
			value = item.CantidadFinal.Value;
		}
		return collection;
	}

	public KardexVM ObtenerUltimoKardex(FilterKardexVM filtro)
	{
		int value = 0;
		ICollection<KardexVM> source = kardexDataAccess.ObtenerKardexPorUniformeTalla(filtro);
		foreach (KardexVM item in source.Reverse())
		{
			item.CantidadInicial = value;
			if (item.TipoRegistro == TipoRegistroKardexEnum.Entrada)
			{
				item.CantidadFinal = item.CantidadInicial + item.Valor;
			}
			else
			{
				item.CantidadFinal = item.CantidadInicial - item.Valor;
			}
			value = item.CantidadFinal.Value;
		}
		if (source.Count() == 0)
		{
			return null;
		}
		return source.First();
	}
}
