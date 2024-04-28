using System;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class ColaboradorRHDTOOut
{
	public int IdColaborador { get; set; }

	public string DNI { get; set; } = string.Empty;


	public string NombreApellidos { get; set; } = string.Empty;


	public string Usuario { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public DateTime FechaIncorporacion { get; set; }

	public RegionDTO Region { get; set; } = new RegionDTO();


	public string Unidad { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;


	public bool Estado { get; set; }
}
