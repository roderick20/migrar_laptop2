using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_Core.ViewModels;

public class PedidoItemVM
{
	public int IdRelacionPedido { get; set; }

	public int IdPedido { get; set; }

	public int IdItemPedido { get; set; }

	public int IdUniforme { get; set; }

	public string Uniforme { get; set; } = string.Empty;


	public int IdTalla { get; set; }

	public string Talla { get; set; } = string.Empty;


	public int Cantidad { get; set; }

	public bool Solicitado { get; set; }

	public TipoPedidoEnum TipoPedido { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
