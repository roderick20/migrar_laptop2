using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;
using CMAC_Bienestar_WebAPI.Exceptions;
using CMAC_Bienestar_WebAPI.Helpers;
using CMAC_Bienestar_WebAPI.Mappers;
using IronPdf;
using IronPdf.Rendering.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class PedidosController : Controller
{
	private readonly IPedidoRepository pedidoRepository;

	private readonly CustomMappers mapper;

	public PedidosController(IPedidoRepository pedidoRepository, IMapper mapper)
	{
		this.pedidoRepository = pedidoRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet("Item")]
	public IEnumerable<ItemPedidoDTOOut> ObtenerItemsPedidos(int idTipoPedido, string usuarioCrea)
	{
		return mapper.ItemPedidoVMListToItemPedidoDTOList(pedidoRepository.ObtenerItemsPedidos(idTipoPedido, usuarioCrea).ToList());
	}

	[HttpGet("ItemPedido")]
	public ItemPedidoDTOOut ObtenerItemPedido(int idUniforme, string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, int tipoItem, string usuario)
	{
		ItemPedidoVM itemPedidoVM = pedidoRepository.ObtenerItemPedido(idUniforme, codigoRegion, codigoSede, codigoUnidad, codigoPuesto, tipoItem, usuario);
		if (itemPedidoVM == null)
		{
			throw new ObjectNullException("No se encontró ningún pedido con lo datos proporcionados.");
		}
		return mapper.ItemPedidoVMToItemPedidoDTOOut(itemPedidoVM);
	}

	[HttpPost("Item")]
	public ActionResult AgregarItemPedido(ItemPedidoDTOCreate itemPedido)
	{
		int num = pedidoRepository.AgregarItemPedido(mapper.ItemPedidoDTOToItemPedidoVM(itemPedido));
		if (num == -1)
		{
			throw new DuplicateObjectException("Ya existe un item con los datos proporcionados.");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Item agregado correctamente."
		});
	}

	[HttpPut("Item/{idItemPedido}")]
	public ActionResult ActualizarItemPedido(int idItemPedido, ItemPedidoDTOUpdate itemPedido)
	{
		ItemPedidoVM itemPedidoVM = mapper.ItemPedidoDTOToItemPedidoVM(itemPedido);
		itemPedidoVM.IdItemPedido = idItemPedido;
		int num = pedidoRepository.ActualizarItemPedido(itemPedidoVM);
		if (num == -1)
		{
			throw new ObjectNullException("No existe ningun item con el id " + idItemPedido + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Item actualizado correctamente."
		});
	}

	[HttpDelete("Item/{idItemPedido}")]
	public ActionResult EliminarItemPedido(int idItemPedido)
	{
		int num = pedidoRepository.EliminarItemPedido(idItemPedido);
		if (num == -1)
		{
			throw new ObjectNullException("No existe ningun item con el id " + idItemPedido + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Item eliminado correctamente."
		});
	}

	[HttpGet("ObtenerPedidos")]
	public IEnumerable<PedidoDTOOut> ObtenerPedidos(int idTipoPedido, string usuarioCrea)
	{
		return mapper.PedidoVMListToPedidoDTOOutList(pedidoRepository.ObtenerPedidos(idTipoPedido, usuarioCrea).ToList());
	}

	[HttpGet("{idPedido}")]
	public PedidoDTOOut ObtenerPedidoPorId(int idPedido)
	{
		PedidoVM pedidoVM = pedidoRepository.ObtenerPedidoPorId(idPedido);
		if (pedidoVM == null)
		{
			throw new ObjectNullException("No se encontró ningún pedido con este id.");
		}
		return mapper.PedidoVMToPedidoDTOOut(pedidoVM);
	}

	[HttpGet("PedidoActual")]
	public PedidoDTOOut ObtenerPedidoActual(int idPeriodo, int tipoPedido, string usuario)
	{
		PedidoVM pedidoVM = pedidoRepository.ObtenerPedidoActual(idPeriodo, tipoPedido, usuario);
		if (pedidoVM == null)
		{
			throw new ObjectNullException("No se encontró ningún pedido para este periodo.");
		}
		return mapper.PedidoVMToPedidoDTOOut(pedidoVM);
	}

	[HttpPost]
	public ActionResult ConfirmarPedido(PedidoDTOCreate pedido)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		string text = "PedidoEmail";
		ChromePdfRenderer val = new ChromePdfRenderer();
		int idPedido = pedidoRepository.ConfirmarPedido(mapper.PedidoDTOCreateToPedidoVM(pedido));
		PedidoVM pedidoVM = pedidoRepository.ObtenerPedidoPorId(idPedido);
		if (pedidoVM == null)
		{
			throw new ObjectNullException("No se encontró ningún pedido con este id.");
		}
		switch (pedido.TipoPedido)
		{
		case 2:
		{
			pedidoVM.Iva = decimal.Parse((Math.Truncate(double.Parse(pedido.Total.Value.ToString()) * 0.21 * 100.0) / 100.0).ToString());
			pedidoVM.TotalIva = pedidoVM.Total + pedidoVM.Iva;
			for (int i = 0; i < pedidoVM.Cuotas; i++)
			{
				pedidoVM.CantidadCuotas.Add(pedidoVM.TotalIva.Value / (decimal)pedidoVM.Cuotas.Value);
			}
			text = "PedidoDescuentoEmail";
			break;
		}
		case 3:
			text = "PedidoEmail";
			break;
		default:
			text = "PedidoEmail";
			break;
		case 1:
			break;
		}
		if (text != null)
		{
			Task<string> task = this.RenderViewAsync(text, pedidoVM);
			PdfDocument val2 = ((BasePdfRenderer)val).RenderHtmlAsPdf(task.Result, (Uri)null, (string)null);
			try
			{
				pedidoRepository.EnviarEmailPedido(new EmailVM
				{
					destinatarios = pedidoVM.Email,
					asunto = "Pedido",
					mensaje = "Pedido"
				}, val2.Stream);
			}
			finally
			{
				((IDisposable)val2)?.Dispose();
			}
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Pedido generado correctamente, email enviado con el resultado."
		});
	}

	[HttpPut("{idPedido}")]
	public ActionResult ActualizarPedido(int idPedido, PedidoDTOUpdate pedido)
	{
		PedidoVM pedidoVM = mapper.PedidoDTOUpdateToPedidoVM(pedido);
		pedidoVM.IdPedido = idPedido;
		int num = pedidoRepository.ActualizarPedido(pedidoVM);
		if (num == -1)
		{
			throw new DuplicateObjectException("Ya existe un item con los datos proporcionados.");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Item actualizado correctamente."
		});
	}

	[HttpDelete]
	public ActionResult EliminarPedido(int idPedido)
	{
		int num = pedidoRepository.EliminarPedido(idPedido);
		if (num == -1)
		{
			throw new ObjectNullException("No existe ningun pedido con el id " + idPedido + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Pedido agregado correctamente."
		});
	}
}
