using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Filters;
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
public class KardexController : Controller
{
	private readonly IKardexRepository kardexRepository;

	private readonly CustomMappers mapper;

	public KardexController(IKardexRepository kardexRepository, IMapper mapper)
	{
		this.kardexRepository = kardexRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpPost("ObtenerKardexPorUniformeTalla")]
	public IEnumerable<KardexDTOOut> ObtenerKardexPorUniforme(FilterKardexVM filtroKardex)
	{
		return mapper.KardexVMListToKardexDTOList(kardexRepository.ObtenerKardexPorUniformeTalla(filtroKardex).ToList());
	}

	[HttpPost("ObtenerUltimoKardex")]
	public KardexDTOOut ObtenerUltimoKardex(FilterKardexVM filtroKardex)
	{
		KardexVM kardexVM = kardexRepository.ObtenerUltimoKardex(filtroKardex);
		if (kardexVM == null)
		{
			return null;
		}
		return mapper.KardexVMToKardexDTO(kardexVM);
	}

	[HttpGet("{idKardex}")]
	public KardexDTOOut ObtenerKardexPorId(int idKardex)
	{
		KardexDTOOut kardexDTOOut = mapper.KardexVMToKardexDTO(kardexRepository.ObtenerKardexPorId(idKardex));
		if (kardexDTOOut == null)
		{
			throw new ObjectNullException("No se encontró ningún kardex con el id: " + idKardex);
		}
		return kardexDTOOut;
	}

	[HttpPost]
	public ActionResult AgregarKardex(KardexDTOCreate kardex)
	{
		kardexRepository.AgregarKardex(mapper.KardexDTOToKardexVM(kardex));
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Kardex agregado correctamente."
		});
	}

	[HttpPut("{idKardex}")]
	public ActionResult ActualizarKardex(int idKardex, KardexDTOUpdate kardexDto)
	{
		KardexVM kardexVM = mapper.KardexDTOToKardexVM(kardexDto);
		kardexVM.IdKardex = idKardex;
		kardexRepository.ActualizarKardex(kardexVM);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Kardex actualizado correctamente."
		});
	}

	[HttpDelete("{idKardex}")]
	public ActionResult EliminarKardex(int idKardex)
	{
		kardexRepository.EliminarKardex(idKardex);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Kardex eliminado correctamente."
		});
	}
}
