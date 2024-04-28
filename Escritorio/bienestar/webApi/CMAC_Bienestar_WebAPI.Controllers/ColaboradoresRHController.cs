using System.Collections.Generic;
using System.IO;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class ColaboradoresRHController : ControllerBase
{
	private readonly IColaboradorRHRepository colaboradorRHRepository;

	private readonly CustomMappers mapper;

	public ColaboradoresRHController(IColaboradorRHRepository colaboradorRHRepository, IMapper mapper)
	{
		this.colaboradorRHRepository = colaboradorRHRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<ColaboradorRHDTOOut> ObtenerColaboradoresRH()
	{
		return mapper.ColaboradorRHVMListToColaboradorRHDTOList(colaboradorRHRepository.ObtenerColaboradoresRH().ToList());
	}

	[HttpGet("{idColaboradorRH}")]
	public ColaboradorRHDTOOut ObtenerColaboradorRHPorId(int idColaboradorRH)
	{
		return mapper.ColaboradorRHVMToColaboradorRHDTO(colaboradorRHRepository.ObtenerColaboradorRHPorId(idColaboradorRH));
	}

	[HttpPost]
	public ActionResult AgregarColaboradorRH(ColaboradorRHDTOCreate colaboradorRH)
	{
		int num = colaboradorRHRepository.AgregarColaboradorRH(mapper.ColaboradorRHDTOToColaboradorRHVM(colaboradorRH));
		if (num == -2)
		{
			throw new DuplicateObjectException("Ya existe un personal de RH con los datos proporcionados.");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Personal RH agregado correctamente."
		});
	}

	[HttpPut("{idColaboradorRH}")]
	public ActionResult ActualizarColaboradorRH(int idColaboradorRH, ColaboradorRHDTOUpdate colaboradorRH)
	{
		ColaboradorRHVM colaboradorRHVM = mapper.ColaboradorRHDTOToColaboradorRHVM(colaboradorRH);
		colaboradorRHVM.IdColaborador = idColaboradorRH;
		return colaboradorRHRepository.ActualizarColaboradorRH(colaboradorRHVM) switch
		{
			-1 => throw new ObjectNullException("No se encontro ningun personal de RH con id = " + idColaboradorRH + "."), 
			-2 => throw new DuplicateObjectException("Ya existe un personal de RH con los datos proporcionados."), 
			_ => Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Personal RH actualizado correctamente."
			}), 
		};
	}

	[HttpDelete("{idColaboradorRH}")]
	public ActionResult EliminarColaboradorRH(int idColaboradorRH)
	{
		int num = colaboradorRHRepository.EliminarColaboradorRH(idColaboradorRH);
		if (num == -1)
		{
			throw new ObjectNullException("No se encontro ningun personal de RH con id = " + idColaboradorRH + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Personal RH eliminado correctamente."
		});
	}

	[HttpPost("CargaMasiva")]
	public ActionResult CargaMasivaColaboradorRH(IFormFile excelFile)
	{
		string text = colaboradorRHRepository.CargaMasivaColaboradorRH(excelFile.OpenReadStream());
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Personal RH cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}

	[HttpGet("DescargarPlantilla")]
	public ActionResult DescargarPlantilla()
	{
		Stream stream = new FileStream("CargaMasiva/PlantillaPersonalRH.xlsx", FileMode.Open, FileAccess.Read);
		if (stream == null)
		{
			throw new ObjectNullException("No se encontro ninguna plantilla.");
		}
		return File(stream, "application/octet-stream", "PlantillaPersonalRH.xlsx");
	}
}
