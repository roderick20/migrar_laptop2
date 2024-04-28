using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;
using CMAC_Bienestar_WebAPI.Exceptions;
using CMAC_Bienestar_WebAPI.Helpers;
using CMAC_Bienestar_WebAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class PeriodosController : ControllerBase
{
	private readonly IPeriodoRepository periodoRepository;

	private readonly CustomMappers mapper;

	public PeriodosController(IPeriodoRepository periodoRepository, IMapper mapper)
	{
		this.periodoRepository = periodoRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<PeriodoDTOOut> ObtenerPeriodos()
	{
		return mapper.PeriodoVMListToPeriodoDTOList(periodoRepository.ObtenerPeriodos().ToList());
	}

	[HttpGet("{idPeriodo}")]
	public PeriodoDTOOut ObtenerPeriodoPorId(int idPeriodo)
	{
		PeriodoDTOOut periodoDTOOut = mapper.PeriodoVMToPeriodoDTO(periodoRepository.ObtenerPeriodoPorId(idPeriodo));
		if (periodoDTOOut == null)
		{
			throw new ObjectNullException("No se encontró ningún periodo con el id: " + idPeriodo);
		}
		return periodoDTOOut;
	}

	[HttpPost]
	public ActionResult AgregarPeriodo(PeriodoDTOCreate periodo)
	{
		periodoRepository.AgregarPeriodo(mapper.PeriodoDTOToPeriodoVM(periodo));
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Periodo agregado correctamente."
		});
	}

	[HttpPut("{idPeriodo}")]
	public ActionResult ActualizarPeriodo(int idPeriodo, PeriodoDTOUpdate periodoDto)
	{
		PeriodoVM periodoVM = mapper.PeriodoDTOToPeriodoVM(periodoDto);
		periodoVM.IdPeriodo = idPeriodo;
		periodoRepository.ActualizarPeriodo(periodoVM);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Periodo actualizado correctamente."
		});
	}

	[HttpDelete("{idPeriodo}")]
	public ActionResult EliminarPeriodo(int idPeriodo)
	{
		periodoRepository.EliminarPeriodo(idPeriodo);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Periodo eliminado correctamente."
		});
	}

	[HttpPost("{idPeriodo}/actualizar-estado")]
	public ActionResult ActualizarEstadoPeriodo(int idPeriodo)
	{
		PeriodoVM periodoVM = periodoRepository.ObtenerPeriodoPorId(idPeriodo);
		if (periodoVM.EstadoPeriodo == EstadoPeriodoEnum.Abierto)
		{
			periodoVM.EstadoPeriodo = EstadoPeriodoEnum.Cerrado;
		}
		else if (periodoVM.EstadoPeriodo == EstadoPeriodoEnum.Cerrado)
		{
			periodoVM.EstadoPeriodo = EstadoPeriodoEnum.Reabierto;
		}
		else if (periodoVM.EstadoPeriodo == EstadoPeriodoEnum.Reabierto)
		{
			periodoVM.EstadoPeriodo = EstadoPeriodoEnum.CerradoDefinitivo;
		}
		periodoRepository.ActualizarEstadoPeriodo(periodoVM);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Periodo cerrado correctamente."
		});
	}

	[HttpGet("Activo")]
	public PeriodoDTOOut ValidarPeriodoActivo(string usuario, int tipoPedido)
	{
		PeriodoDTOOut periodoDTOOut = mapper.PeriodoVMToPeriodoDTO(periodoRepository.ObtenerPeriodoActivo(usuario, tipoPedido));
		if (periodoDTOOut == null)
		{
			throw new ObjectNullException("No se encontró ningún periodo activo.");
		}
		return periodoDTOOut;
	}
}
