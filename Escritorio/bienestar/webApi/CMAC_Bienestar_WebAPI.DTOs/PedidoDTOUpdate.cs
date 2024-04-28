namespace CMAC_Bienestar_WebAPI.DTOs;

public class PedidoDTOUpdate
{
	public decimal? Total { get; set; }

	public int? Cuotas { get; set; }

	public string UsuarioModi { get; set; } = string.Empty;

}
