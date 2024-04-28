namespace CMAC_Bienestar_Core.ViewModels.Reportes;

public class ReporteDsctPlanillaUniformeVM
{
	public int IdGrupo { get; set; }

	public int IdUniforme { get; set; }

	public int Solicitado { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;


	public string NombreUniforme { get; set; } = string.Empty;


	public int Cantidad { get; set; }

	public string Talla { get; set; } = string.Empty;

}
