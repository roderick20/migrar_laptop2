using System.Linq;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class BannerRepository : IBannerRepository
{
	private readonly BannerDataAccess bannerDataAccess;

	public BannerRepository(IConfiguration configuration)
	{
		bannerDataAccess = new BannerDataAccess(configuration);
	}

	public int AgregarImagenBanner(ImagenBannerVM imagenBanner)
	{
		return bannerDataAccess.AgregarImagenBanner(imagenBanner);
	}

	public BannerVM ObtenerBanner()
	{
		BannerVM bannerVM = new BannerVM();
		bannerVM.Imagenes = bannerDataAccess.ObtenerImagenesBanner().ToList();
		return bannerVM;
	}

	public int EliminarImagenBanner(int idImagenBanner)
	{
		return bannerDataAccess.EliminarImagenBanner(idImagenBanner);
	}
}
