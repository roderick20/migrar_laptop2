using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_WebAPI.DTOs;
using CMAC_Bienestar_WebAPI.Exceptions;
using CMAC_Bienestar_WebAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class ExtrasController : ControllerBase
{
	private readonly IExtrasRepository extrasRepository;

	private readonly CustomMappers mapper;

	public ExtrasController(IExtrasRepository extrasRepository, IMapper mapper)
	{
		this.extrasRepository = extrasRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet("ObtenerSedes")]
	public IEnumerable<SedeDTO> ObtenerSedes()
	{
		return mapper.SedeVMToSedeDTO(extrasRepository.ObtenerSedes().ToList());
	}

	[HttpGet("ObtenerUnidades")]
	public IEnumerable<UnidadDTO> ObtenerUnidades()
	{
		return mapper.UnidadVMToUnidadDTO(extrasRepository.ObtenerUnidades().ToList());
	}

	[HttpGet("ObtenerPuestos")]
	public IEnumerable<PuestoDTO> ObtenerPuestos()
	{
		return mapper.PuestoVMToPuestoDTO(extrasRepository.ObtenerPuestos().ToList());
	}
}
