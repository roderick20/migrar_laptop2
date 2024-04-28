using System;

namespace CMAC_Bienestar_Core.ViewModels;

public class TrabajadorVM
{
	public int? IdColaborador { get; set; }

	public string employee_id { get; set; } = string.Empty;


	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public string Dni { get; set; } = string.Empty;


	public string CodigoUnidad { get; set; } = string.Empty;


	public string Unidad { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string CodigoPuesto { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public DateTime? FechaIngresoCorporacion { get; set; }
}
