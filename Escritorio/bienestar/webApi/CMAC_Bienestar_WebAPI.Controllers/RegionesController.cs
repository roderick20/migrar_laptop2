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
public class RegionesController : ControllerBase
{
	private readonly IRegionRepository regionRepository;

	private readonly CustomMappers mapper;

	public RegionesController(IRegionRepository regionRepository, IMapper mapper)
	{
		this.regionRepository = regionRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<RegionDTO> ObtenerRegiones()
	{
		return mapper.RegionVMListToRegionDTOList(regionRepository.ObtenerRegiones().ToList());
	}
}
