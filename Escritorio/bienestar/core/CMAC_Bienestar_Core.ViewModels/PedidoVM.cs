using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class PedidoVM : AuditoriaVM
{
	public int IdPedido { get; set; }

	public decimal? Total { get; set; }

	public decimal? TotalIva { get; set; }

	public decimal? Iva { get; set; }

	public List<decimal> CantidadCuotas { get; set; } = new List<decimal>();


	public int? Cuotas { get; set; }

	public int TipoPedido { get; set; }

	public int EstadoPedido { get; set; } = 2;


	public string Nombre { get; set; } = string.Empty;


	public string Email { get; set; } = string.Empty;


	public List<ItemPedidoVM> ItemsPedidos { get; set; } = new List<ItemPedidoVM>();

}
