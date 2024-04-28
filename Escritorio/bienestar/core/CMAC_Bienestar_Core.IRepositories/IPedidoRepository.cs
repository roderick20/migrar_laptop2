using System.Collections.Generic;
using System.IO;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IPedidoRepository
{
	ICollection<ItemPedidoVM> ObtenerItemsPedidos(int idTipoPedido, string usuarioCrea);

	ItemPedidoVM? ObtenerItemPedido(int idUniforme, string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, int tipoItem, string usuario);

	int AgregarItemPedido(ItemPedidoVM itemPedido);

	int EliminarItemPedido(int idItemPedido);

	int ActualizarItemPedido(ItemPedidoVM itemPedido);

	ICollection<PedidoVM> ObtenerPedidos(int idTipoPedido, string usuarioCrea);

	PedidoVM? ObtenerPedidoPorId(int idPedido);

	int ConfirmarPedido(PedidoVM pedido);

	int ActualizarPedido(PedidoVM pedido);

	int EliminarPedido(int idPedido);

	PedidoVM? ObtenerPedidoActual(int idPeriodo, int tipoPedido, string usuario);

	bool EnviarEmailPedido(EmailVM email);

	bool EnviarEmailPedido(EmailVM email, Stream stream);
}
