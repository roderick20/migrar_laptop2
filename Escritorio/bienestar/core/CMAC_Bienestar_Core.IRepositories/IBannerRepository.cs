using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IBannerRepository
{
	int AgregarImagenBanner(ImagenBannerVM imagenBanner);

	BannerVM ObtenerBanner();

	int EliminarImagenBanner(int idImagenBanner);
}
