using System.Collections.Generic;
using System.Linq;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class UniformeRepository : IUniformeRepository
{
	private readonly UniformeDataAccess uniformeDataAccess;

	private readonly TallaDataAccess tallaDataAccess;

	private readonly KardexDataAccess kardexDataAccess;

	public UniformeRepository(IConfiguration configuration)
	{
		uniformeDataAccess = new UniformeDataAccess(configuration);
		tallaDataAccess = new TallaDataAccess(configuration);
		kardexDataAccess = new KardexDataAccess(configuration);
	}

	public int ActualizarUniforme(UniformeVM uniforme)
	{
		int num = uniformeDataAccess.ActualizarUniforme(uniforme);
		if (num != -1 && num != -2)
		{
			tallaDataAccess.EliminarTallasPorUniforme(num);
			int[] idsTallas = uniforme.IdsTallas;
			foreach (int idTalla in idsTallas)
			{
				tallaDataAccess.AgregarTallasPorUniforme(idTalla, num);
			}
		}
		return num;
	}

	public int AgregarUniforme(UniformeVM uniforme)
	{
		int num = uniformeDataAccess.AgregarUniforme(uniforme);
		if (num != -1 && num != -2)
		{
			int[] idsTallas = uniforme.IdsTallas;
			foreach (int idTalla in idsTallas)
			{
				tallaDataAccess.AgregarTallasPorUniforme(idTalla, num);
			}
		}
		return num;
	}

	public int EliminarUniforme(int idUniforme)
	{
		return uniformeDataAccess.EliminarUniforme(idUniforme);
	}

	public UniformeVM? ObtenerUniformePorId(int idUniforme)
	{
		return uniformeDataAccess.ObtenerUniformePorId(idUniforme);
	}

	public ICollection<UniformeVM> ObtenerUniformes()
	{
		return uniformeDataAccess.ObtenerUniformes();
	}

	public ICollection<UniformeVM> ObtenerUniformesSimple()
	{
		return uniformeDataAccess.ObtenerUniformesSimple();
	}

	public ICollection<UniformeKardexVM> ObtenerUniformesConKardex()
	{
		ICollection<UniformeVM> collection = uniformeDataAccess.ObtenerUniformesSimple();
		List<UniformeKardexVM> list = new List<UniformeKardexVM>();
		foreach (UniformeVM item in collection)
		{
			UniformeKardexVM uniformeKardexVM = new UniformeKardexVM();
			uniformeKardexVM.IdUniforme = item.IdUniforme;
			uniformeKardexVM.Nombre = item.Nombre;
			uniformeKardexVM.Tallas = kardexDataAccess.ObtenerTallasConCantidadPorUniforme(item.IdUniforme).ToList();
			list.Add(uniformeKardexVM);
		}
		return list;
	}

	public ICollection<UniformeVM> ObtenerUniformesPorGrupo(string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, string sexo)
	{
		return uniformeDataAccess.ObtenerUniformesPorGrupo(codigoRegion, codigoSede, codigoUnidad, codigoPuesto, sexo);
	}

	public UniformeVM? ObtenerUniformePorNombre(string Uniforme_name)
	{
		return uniformeDataAccess.ObtenerUniformePorNombre(Uniforme_name);
	}
}
