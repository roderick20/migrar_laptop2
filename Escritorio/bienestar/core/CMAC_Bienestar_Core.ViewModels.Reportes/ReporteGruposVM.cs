namespace CMAC_Bienestar_Core.ViewModels.Reportes;

public class ReporteGruposVM
{
	public int IdGrupo { get; set; }

	public string Grupo { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;


	public string Genero { get; set; } = string.Empty;


	public string Region { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public int Solicitado { get; set; }

	public int Existencias { get; set; }

	public int TotalPrendas { get; set; }

	public int AsignadoFemenino { get; set; }

	public int SolicitadoFemenino { get; set; }

	public int AsignadoMasculino { get; set; }

	public int SolicitadoMasculino { get; set; }

	public int TotalSolicitantes { get; set; }
}
