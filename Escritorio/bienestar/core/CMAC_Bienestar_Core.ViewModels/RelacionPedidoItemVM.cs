namespace CMAC_Bienestar_Core.ViewModels;

public class RelacionPedidoItemVM
{
	public int IdRelacionPedido { get; set; }

	public int IdPedido { get; set; }

	public PedidoVM PedidoCabecera { get; set; } = new PedidoVM();


	public int IdItemPedido { get; set; }

	public ItemPedidoVM PedidoDetalle { get; set; } = new ItemPedidoVM();

}
