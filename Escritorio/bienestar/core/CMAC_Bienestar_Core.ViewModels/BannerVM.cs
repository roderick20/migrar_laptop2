using System.Collections.Generic;

namespace CMAC_Bienestar_Core.ViewModels;

public class BannerVM
{
	public string VideoLink { get; set; } = string.Empty;


	public List<ImagenBannerVM> Imagenes { get; set; } = new List<ImagenBannerVM>();

}
