using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class BannerDTOCreate
{
	public List<string> GUIDImagenes { get; set; } = new List<string>();


	public int[]? IdsImagenesBannerEliminar { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
