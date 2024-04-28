namespace CMAC_Bienestar_Core.ViewModels.Reportes;

public class ReporteUniformesVM
{
	public string Uniforme { get; set; } = string.Empty;


	public int IdTalla { get; set; }

	public string Talla { get; set; } = string.Empty;


	public int Masculino { get; set; }

	public int Femenino { get; set; }

	public int Cantidad { get; set; }
}
