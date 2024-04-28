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
public class TallasController : ControllerBase
{
	private readonly ITallaRepository tallaRepository;

	private readonly CustomMappers mapper;

	public TallasController(ITallaRepository tallaRepository, IMapper mapper)
	{
		this.tallaRepository = tallaRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<TallaDTO> ObtenerUniformes()
	{
		return mapper.TallaVMListToTallaDTOList(tallaRepository.ObtenerTodasTallas().ToList());
	}

	[HttpGet("ObtenerPorUniforme")]
	public IEnumerable<TallaDTO> ObtenerTallasPorUniforme(int idUniforme)
	{
		return mapper.TallaVMListToTallaDTOList(tallaRepository.ObtenerTallasPorUniforme(idUniforme).ToList());
	}
}
