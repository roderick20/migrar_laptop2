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
public class UniformesController : ControllerBase
{
	private readonly IUniformeRepository uniformeRepository;

	private readonly CustomMappers mapper;

	public UniformesController(IUniformeRepository uniformeRepository, IMapper mapper)
	{
		this.uniformeRepository = uniformeRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<UniformeDTOOut> ObtenerUniformes()
	{
		return mapper.UniformeVMListToUniformeDTOList(uniformeRepository.ObtenerUniformes().ToList());
	}

	[HttpGet("simple")]
	public IEnumerable<UniformeDTOOut> ObtenerUniformesSimple()
	{
		return mapper.UniformeVMListToUniformeDTOList(uniformeRepository.ObtenerUniformesSimple().ToList());
	}

	[HttpGet("ObtenerUniformesConKardex")]
	public IEnumerable<UniformeKardexDTOOut> ObtenerUniformesConKardex()
	{
		return mapper.UniformeKardexVMListToUniformeKardexDTOList(uniformeRepository.ObtenerUniformesConKardex().ToList());
	}

	[HttpGet("{idUniforme}")]
	public UniformeDTOOut ObtenerUniformePorId(int idUniforme)
	{
		UniformeVM uniformeVM = uniformeRepository.ObtenerUniformePorId(idUniforme);
		if (uniformeVM == null)
		{
			throw new ObjectNullException("No se encontro ningún uniforme con el id: " + idUniforme);
		}
		return mapper.UniformeVMToUniformeDTO(uniformeVM);
	}

	[HttpGet("ObtenerUniformesPorGrupo")]
	public IEnumerable<UniformePedidoDTOOut> ObtenerUniformesPorGrupo(string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, string sexo)
	{
		return mapper.UniformeVMListToUniformePedidoDTOList(uniformeRepository.ObtenerUniformesPorGrupo(codigoRegion, codigoSede, codigoUnidad, codigoPuesto, sexo).ToList());
	}

	[HttpPost]
	public ActionResult AgregarUniforme(UniformeDTOCreate uniforme)
	{
		int num = uniformeRepository.AgregarUniforme(mapper.UniformeDTOToUniformeVM(uniforme));
		if (num == -2)
		{
			throw new DuplicateObjectException("Ya existe un uniforme con los datos proporcionados.");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Uniforme agregado correctamente."
		});
	}

	[HttpPut("{idUniforme}")]
	public ActionResult ActualizarUniforme(int idUniforme, UniformeDTOUpdate uniforme)
	{
		UniformeVM uniformeVM = mapper.UniformeDTOToUniformeVM(uniforme);
		uniformeVM.IdUniforme = idUniforme;
		return uniformeRepository.ActualizarUniforme(uniformeVM) switch
		{
			-1 => throw new ObjectNullException("No se encontro ningun uniforme con id = " + idUniforme + "."), 
			-2 => throw new DuplicateObjectException("Ya existe un uniforme con los datos proporcionados."), 
			_ => Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Uniforme actualizado correctamente."
			}), 
		};
	}

	[HttpDelete("{idUniforme}")]
	public ActionResult EliminarUniforme(int idUniforme)
	{
		int num = uniformeRepository.EliminarUniforme(idUniforme);
		if (num == -1)
		{
			throw new ObjectNullException("No se encontro ningun uniforme con id = " + idUniforme + ".");
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Uniforme eliminado correctamente."
		});
	}
}
