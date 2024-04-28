using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class PedidoDTOOut
{
	public int IdPedido { get; set; }

	public decimal? Total { get; set; }

	public int? Cuotas { get; set; }

	public int? TipoPedido { get; set; }

	public int EstadoPedido { get; set; } = 2;


	public string UsuarioCrea { get; set; } = string.Empty;


	public List<ItemPedidoDTOOut> ItemsPedidos { get; set; } = new List<ItemPedidoDTOOut>();

}
