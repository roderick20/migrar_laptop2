namespace CMAC_Bienestar_WebAPI.DTOs;

public class UniformeDTOUpdate
{
	public string Nombre { get; set; } = string.Empty;


	public string SKU { get; set; } = string.Empty;


	public int[]? IdsTallas { get; set; }

	public string Genero { get; set; } = string.Empty;


	public int IdTallaEstandar { get; set; }

	public string GUIDImagen { get; set; } = string.Empty;


	public string Detalle { get; set; } = string.Empty;


	public string GUIDDetalle { get; set; } = string.Empty;


	public decimal Precio { get; set; }

	public bool Estado { get; set; }
}
