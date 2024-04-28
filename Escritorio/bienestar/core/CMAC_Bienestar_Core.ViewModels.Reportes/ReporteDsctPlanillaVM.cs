using System;

namespace CMAC_Bienestar_Core.ViewModels.Reportes;

public class ReporteDsctPlanillaVM
{
	public string Usuario { get; set; } = string.Empty;


	public string NumeroTrabajador { get; set; } = string.Empty;


	public string DNI { get; set; } = string.Empty;


	public string Region { get; set; } = string.Empty;


	public string Unidad { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string ApellidosNombres { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public string PuestoTrabajador { get; set; } = string.Empty;


	public DateTime FechaIngreso { get; set; }

	public int IdGrupo { get; set; }

	public string NombreGrupo { get; set; } = string.Empty;


	public int Total { get; set; }

	public int Cuotas { get; set; }

	public int TotalPrendas { get; set; }

	public int Estado { get; set; }
}
