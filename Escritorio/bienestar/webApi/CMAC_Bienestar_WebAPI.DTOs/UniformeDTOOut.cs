using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class UniformeDTOOut
{
	public int IdUniforme { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string SKU { get; set; } = string.Empty;


	public List<TallaDTO> Tallas { get; set; } = new List<TallaDTO>();


	public string Genero { get; set; } = string.Empty;


	public TallaDTO TallaEstandar { get; set; } = new TallaDTO();


	public string GUIDImagen { get; set; } = string.Empty;


	public string Detalle { get; set; } = string.Empty;


	public string GUIDDetalle { get; set; } = string.Empty;


	public decimal Precio { get; set; }

	public bool Estado { get; set; }
}
