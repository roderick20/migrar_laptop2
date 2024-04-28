using System.Collections.Generic;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class RegionRepository : IRegionRepository
{
	private readonly RegionDataAccess regionDataAccess;

	public RegionRepository(IConfiguration configuration)
	{
		regionDataAccess = new RegionDataAccess(configuration);
	}

	public IEnumerable<RegionVM> ObtenerRegiones()
	{
		return regionDataAccess.ObtenerRegiones();
	}
}
