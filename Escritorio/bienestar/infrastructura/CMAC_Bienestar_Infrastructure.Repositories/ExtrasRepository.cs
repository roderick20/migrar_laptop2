using System.Collections.Generic;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class ExtrasRepository : IExtrasRepository
{
	private readonly ExtrasDataAccess extrasDataAccess;

	public ExtrasRepository(IConfiguration configuration)
	{
		extrasDataAccess = new ExtrasDataAccess(configuration);
	}

	public ICollection<PuestoVM> ObtenerPuestos()
	{
		return extrasDataAccess.ObtenerPuestos();
	}

	public ICollection<SedeVM> ObtenerSedes()
	{
		return extrasDataAccess.ObtenerSedes();
	}

	public ICollection<UnidadVM> ObtenerUnidades()
	{
		return extrasDataAccess.ObtenerUnidades();
	}
}
