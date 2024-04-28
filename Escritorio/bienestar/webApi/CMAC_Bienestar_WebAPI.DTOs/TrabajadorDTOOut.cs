using System;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class TrabajadorDTOOut
{
	public string IdTrabajador { get; set; } = string.Empty;


	public int? IdColaborador { get; set; }

	public string employee_id { get; set; } = string.Empty;


	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public string Dni { get; set; } = string.Empty;


	public string Email { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string CodigoUnidad { get; set; } = string.Empty;


	public string CodigoPuesto { get; set; } = string.Empty;


	public DateTime? FechaIncorporacion { get; set; }

	public string Sexo { get; set; } = string.Empty;

}
