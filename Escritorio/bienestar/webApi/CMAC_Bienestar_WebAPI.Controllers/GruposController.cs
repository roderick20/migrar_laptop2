using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Base;
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
public class GruposController : ControllerBase
{
	private readonly IGrupoRepository grupoRepository;

	private readonly CustomMappers mapper;

	public GruposController(IGrupoRepository grupoRepository, IMapper mapper)
	{
		this.grupoRepository = grupoRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<GrupoDTOOut> ObtenerGrupos()
	{
		return mapper.GrupoVMListToGrupoDTOList(grupoRepository.ObtenerGrupos().ToList());
	}

	[HttpGet("{idGrupo}")]
	public GrupoDTOOut ObtenerGrupoPorId(int idGrupo)
	{
		GrupoVM grupoVM = grupoRepository.ObtenerGrupoPorId(idGrupo);
		if (grupoVM == null)
		{
			throw new ObjectNullException("No se encontró ningún grupo con el id: " + idGrupo);
		}
		return mapper.GrupoVMToGrupoDTO(grupoVM);
	}

	[HttpPost]
	public ActionResult AgregarGrupo(GrupoDTOCreate grupo)
	{
		ResponseBase responseBase = grupoRepository.AgregarGrupo(mapper.GrupoDTOToGrupoVM(grupo));
		if (responseBase.Codigo == -2 || responseBase.Codigo == -3)
		{
			throw new DuplicateObjectException(responseBase.Mensaje);
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Grupo agregado correctamente."
		});
	}

	[HttpPut("{idGrupo}")]
	public ActionResult ActualizarGrupo(int idGrupo, GrupoDTOUpdate grupo)
	{
		GrupoVM grupoVM = mapper.GrupoDTOToGrupoVM(grupo);
		grupoVM.IdGrupo = idGrupo;
		ResponseBase responseBase = grupoRepository.ActualizarGrupo(grupoVM);
		if (responseBase.Codigo == -1)
		{
			throw new ObjectNullException(responseBase.Mensaje);
		}
		if (responseBase.Codigo == -2 || responseBase.Codigo == -3)
		{
			throw new DuplicateObjectException(responseBase.Mensaje);
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Grupo actualizado correctamente."
		});
	}

	[HttpDelete("{idGrupo}")]
	public ActionResult EliminarGrupo(int idGrupo)
	{
		int num = grupoRepository.EliminarGrupo(idGrupo);
		if (num == -1)
		{
			throw new ObjectNullException("No se encontro ningun grupo con id = " + idGrupo + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Grupo eliminado correctamente."
		});
	}

	[HttpPost("CargaMasiva")]
	public ActionResult CargaMasivaGrupo(IFormFile excelFile)
	{
		string text = grupoRepository.CargaMasivaGrupo(excelFile.OpenReadStream());
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Grupos cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}

	[HttpGet("DescargarPlantilla")]
	public ActionResult DescargarPlantilla()
	{
		Stream stream = new FileStream("CargaMasiva/PlantillaGrupo.xlsx", FileMode.Open, FileAccess.Read);
		if (stream == null)
		{
			throw new ObjectNullException("No se encontro ninguna plantilla.");
		}
		return File(stream, "application/octet-stream", "PlantillaGrupo.xlsx");
	}

	[HttpPost("CargarDataInicialGrupos")]
	public ActionResult CargarDataInicialGrupos()
	{
		string text = grupoRepository.CargarDataInicialGrupos();
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Grupos cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}

	[HttpPost("CargarDataInicialGruposVersion2")]
	public ActionResult CargarDataInicialGruposVersion2()
	{
		string text = grupoRepository.CargarDataInicialGruposVersion2();
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Grupos V.2. cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}

	[HttpPost("CargarDataInicialUniformesGrupo")]
	public ActionResult CargarDataInicialGruposxUniformes()
	{
		string text = grupoRepository.CargarDataInicialGruposxUniformes();
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Uniformes de Grupos cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}
}
