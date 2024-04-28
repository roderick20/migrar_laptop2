using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using CMAC_Bienestar_Infrastructure.Emails;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
	private readonly PedidoDataAccess pedidoDataAccess;

	private readonly UniformeDataAccess uniformeDataAccess;

	private readonly TallaDataAccess tallaDataAccess;

	private readonly PeriodoDataAccess periodoDataAccess;

	private readonly KardexDataAccess kardexDataAccess;

	private readonly EmailConfig emailConfig;

	public PedidoRepository(IConfiguration configuration)
	{
		pedidoDataAccess = new PedidoDataAccess(configuration);
		uniformeDataAccess = new UniformeDataAccess(configuration);
		tallaDataAccess = new TallaDataAccess(configuration);
		periodoDataAccess = new PeriodoDataAccess(configuration);
		kardexDataAccess = new KardexDataAccess(configuration);
		emailConfig = new EmailConfig(configuration);
	}

	public ItemPedidoVM? ObtenerItemPedido(int idUniforme, string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, int tipoItem, string usuario)
	{
		return pedidoDataAccess.ObtenerItemPedido(idUniforme, codigoRegion, codigoSede, codigoUnidad, codigoPuesto, tipoItem, usuario);
	}

	public ICollection<ItemPedidoVM> ObtenerItemsPedidos(int idTipoPedido, string usuarioCrea)
	{
		return pedidoDataAccess.ObtenerItemsPedidos(idTipoPedido, usuarioCrea);
	}

	public int ActualizarItemPedido(ItemPedidoVM itemPedido)
	{
		return pedidoDataAccess.ActualizarItemPedido(itemPedido);
	}

	public int AgregarItemPedido(ItemPedidoVM itemPedido)
	{
		return pedidoDataAccess.AgregarItemPedido(itemPedido);
	}

	public int EliminarItemPedido(int idItemPedido)
	{
		return pedidoDataAccess.EliminarItemPedido(idItemPedido);
	}

	public int ConfirmarPedido(PedidoVM pedido)
	{
		ICollection<ItemPedidoVM> collection = pedidoDataAccess.ObtenerItemsPedidosSimple(pedido.TipoPedido, pedido.UsuarioCrea);
		if (pedido.TipoPedido == 3 || pedido.TipoPedido == 4)
		{
			foreach (ItemPedidoVM item in collection)
			{
				int idKardex = kardexDataAccess.AgregarKardex(new KardexVM
				{
					IdUniforme = item.IdUniforme,
					IdTalla = item.IdTalla,
					FechaRegistro = DateTime.UtcNow,
					Concepto = "PEDIDO POR KARDEX",
					TipoRegistro = TipoRegistroKardexEnum.Salida,
					Valor = item.Cantidad,
					UsuarioCrea = item.UsuarioCrea
				});
				pedidoDataAccess.ActualizarKardexPedido(item.IdItemPedido, idKardex);
			}
		}
		return pedidoDataAccess.ConfirmarPedido(pedido);
	}

	public int ActualizarPedido(PedidoVM pedido)
	{
		return pedidoDataAccess.ActualizarPedido(pedido);
	}

	public int EliminarPedido(int idPedido)
	{
		return pedidoDataAccess.EliminarPedido(idPedido);
	}

	public ICollection<PedidoVM> ObtenerPedidos(int idTipoPedido, string usuarioCrea)
	{
		return pedidoDataAccess.ObtenerPedidos(idTipoPedido, usuarioCrea);
	}

	public PedidoVM? ObtenerPedidoPorId(int idPedido)
	{
		PedidoVM pedidoVM = pedidoDataAccess.ObtenerPedidoPorIdPedido(idPedido);
		if (pedidoVM != null)
		{
			pedidoVM.ItemsPedidos = pedidoDataAccess.ObtenerItemsPorIdPedido(idPedido).ToList();
			foreach (ItemPedidoVM itemsPedido in pedidoVM.ItemsPedidos)
			{
				itemsPedido.Uniforme = uniformeDataAccess.ObtenerUniformePorId(itemsPedido.IdUniforme);
			}
		}
		return pedidoVM;
	}

	public PedidoVM? ObtenerPedidoActual(int idPeriodo, int tipoPedido, string usuario)
	{
		return pedidoDataAccess.ObtenerPedidoActual(idPeriodo, tipoPedido, usuario);
	}

	public bool EnviarEmailPedido(EmailVM email)
	{
		return emailConfig.EnviarCorreo(email);
	}

	public bool EnviarEmailPedido(EmailVM email, Stream stream)
	{
		return emailConfig.EnviarCorreo(email, stream, "ResumenPedido.pdf");
	}
}
