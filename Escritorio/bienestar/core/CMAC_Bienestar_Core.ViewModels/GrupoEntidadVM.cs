using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class GrupoEntidadVM : AuditoriaVM
{
	public int IdGrupo { get; set; }

	public int IdGrupoEntidad { get; set; }

	public string CodigoRegion { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string CodigoUnidad { get; set; } = string.Empty;


	public string CodigoPuesto { get; set; } = string.Empty;

}
