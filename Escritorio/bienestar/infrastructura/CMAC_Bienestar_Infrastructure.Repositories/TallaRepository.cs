using System.Collections.Generic;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class TallaRepository : ITallaRepository
{
	private readonly TallaDataAccess tallaDataAccess;

	public TallaRepository(IConfiguration configuration)
	{
		tallaDataAccess = new TallaDataAccess(configuration);
	}

	public ICollection<TallaVM> ObtenerTodasTallas()
	{
		return tallaDataAccess.ObtenerTodasTallas();
	}

	public ICollection<TallaVM> ObtenerTallasPorUniforme(int idUniforme)
	{
		return tallaDataAccess.ObtenerTallasPorUniforme(idUniforme);
	}
}
