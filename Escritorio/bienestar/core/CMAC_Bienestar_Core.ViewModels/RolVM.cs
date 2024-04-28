using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class RolVM : AuditoriaVM
{
	public int IdRol { get; set; }

	public string Nombre { get; set; } = string.Empty;

}
