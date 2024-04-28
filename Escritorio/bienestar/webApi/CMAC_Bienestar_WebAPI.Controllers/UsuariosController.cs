using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using CMAC_Bienestar_Core.Common;
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
public class UsuariosController : Controller
{
	private readonly IUsuarioRepository usuarioRepository;

	private readonly CustomMappers mapper;

	public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper)
	{
		this.usuarioRepository = usuarioRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public IEnumerable<UsuarioDTOOut> ObtenerUsuarios()
	{
		return mapper.UsuarioVMListToUsuarioDTOList(usuarioRepository.ObtenerUsuarios().ToList());
	}

	[HttpPost]
	public ActionResult AgregarUsuario(UsuarioDtoIn usuario)
	{
		usuario.Password = Util.EncodePassword(usuario.Password);
		usuarioRepository.AgregarUsuario(mapper.UsuarioDTOToUsuarioVM(usuario));
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Usuario agregado correctamente."
		});
	}

	[HttpPut("{idUsuario}")]
	public ActionResult ActualizarUsuario(int idUsuario, UsuarioDtoIn usuarioDTO)
	{
		UsuarioVM usuarioVM = mapper.UsuarioDTOToUsuarioVM(usuarioDTO);
		usuarioVM.IdUsuario = idUsuario;
		if (!string.IsNullOrEmpty(usuarioVM.Password))
		{
			usuarioVM.Password = Util.EncodePassword(usuarioVM.Password);
		}
		usuarioRepository.ActualizarUsuario(usuarioVM);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Usuario actualizado correctamente."
		});
	}

	[HttpDelete("{idUsuario}")]
	public ActionResult EliminarUsuario(int idUsuario)
	{
		usuarioRepository.ActualizarEstadoUsuario(idUsuario, estado: false);
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Usuario eliminado correctamente."
		});
	}

	[HttpPost("CargaMasiva")]
	public ActionResult CargaMasivaUsuarios(IFormFile excelFile)
	{
		string text = usuarioRepository.CargaMasivaUsuarios(excelFile.OpenReadStream());
		if (text == "")
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Success,
				Message = "Usuarios cargados correctamente."
			});
		}
		throw new BulkLoadException(text);
	}

	[HttpGet("DescargarPlantilla")]
	public ActionResult DescargarPlantilla()
	{
		Stream stream = new FileStream("CargaMasiva/PlantillaUsuarios.xlsx", FileMode.Open, FileAccess.Read);
		if (stream == null)
		{
			throw new ObjectNullException("No se encontro ninguna plantilla.");
		}
		return File(stream, "application/octet-stream", "PlantillaUsuarios.xlsx");
	}
}
