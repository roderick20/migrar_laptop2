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
public class TrabajadoresController : Controller
{
	private readonly ITrabajadorRepository trabajadorRepository;

	private readonly CustomMappers mapper;

	public TrabajadoresController(ITrabajadorRepository trabajadorRepository, IMapper mapper)
	{
		this.trabajadorRepository = trabajadorRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<TrabajadorDTOOut> ObtenerTrabajadores()
	{
		return mapper.TrabajadorVMListToTrabajadorDTOList(trabajadorRepository.ObtenerTrabajadores().ToList());
	}

	[HttpGet("ObtenerTrabajadoresYColaboradoresSinUsuarios")]
	public IEnumerable<TrabajadorDTOOut> ObtenerTrabajadoresYColaboradoresSinUsuarios()
	{
		return mapper.TrabajadorVMListToTrabajadorDTOList(trabajadorRepository.ObtenerTrabajadoresYColaboradoresSinUsuarios().ToList());
	}
}
