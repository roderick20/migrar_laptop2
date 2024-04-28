namespace CMAC_Bienestar_WebAPI.DTOs;

public class PedidoDTOCreate
{
	public decimal? Total { get; set; }

	public int? Cuotas { get; set; }

	public int? TipoPedido { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
