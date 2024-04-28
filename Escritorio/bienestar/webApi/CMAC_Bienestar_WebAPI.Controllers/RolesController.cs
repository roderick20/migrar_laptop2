using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
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
public class RolesController : Controller
{
	private readonly IRolRepository rolRepository;

	private readonly CustomMappers mapper;

	public RolesController(IRolRepository rolRepository, IMapper mapper)
	{
		this.rolRepository = rolRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<RolDTOOut> ObtenerRoles()
	{
		return mapper.RolVMListToRolDTOList(rolRepository.ObtenerRoles().ToList());
	}

	[HttpPost]
	public ActionResult AgregarRol(RolDTOCreate rol)
	{
		rolRepository.AgregarRol(mapper.RolDTOToRolVM(rol));
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Rol agregado correctamente."
		});
	}
}
