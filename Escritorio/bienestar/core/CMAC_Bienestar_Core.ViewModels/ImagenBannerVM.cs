using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class ImagenBannerVM : AuditoriaVM
{
	public int IdImagenBanner { get; set; }

	public string GUIDImagen { get; set; } = string.Empty;

}
