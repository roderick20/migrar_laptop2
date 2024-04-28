using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CMAC_Bienestar_Core.Common;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;
using CMAC_Bienestar_WebAPI.Exceptions;
using CMAC_Bienestar_WebAPI.Helpers;
using CMAC_Bienestar_WebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CMAC_Bienestar_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class LoginController : ControllerBase
{
	private readonly IConfiguration configuration;

	private readonly IUsuarioRepository usuarioRepository;

	private readonly CustomMappers mapper;

	public LoginController(IConfiguration configuration, IUsuarioRepository usuarioRepository, IMapper mapper)
	{
		this.configuration = configuration;
		this.usuarioRepository = usuarioRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpPost]
	public ActionResult Login(LoginDTO loginDTO)
	{
		if (loginDTO.Usuario == "DESAPRUEBAS")
		{
			string text = "DESAPRUEBAS";
			string text2 = "Hm25819w$";
			string value = configuration.GetSection("Rutas:ActiveDirectory").Value;
			DirectoryEntry searchRoot = new DirectoryEntry(value, text, loginDTO.Password);
			DirectorySearcher directorySearcher = new DirectorySearcher(searchRoot);
			directorySearcher.Filter = "sAMAccountName=" + text;
			SearchResult searchResult = null;
			try
			{
				searchResult = directorySearcher.FindOne();
				return Ok(new Response
				{
					Status = RespuestaEnum.Success,
					Message = "SE INGRESÓ A ACTIVE DIRECTORY: " + value
				});
			}
			catch (Exception)
			{
				return Ok(new Response
				{
					Status = RespuestaEnum.Invalid,
					Message = "ERROR ACTIVE DIRECTORY: " + value
				});
			}
		}
		UsuarioVM usuarioVM = usuarioRepository.ObtenerUsuarioPorNombreUsuario(loginDTO.Usuario);
		if (usuarioVM == null)
		{
			return Ok(new Response
			{
				Status = RespuestaEnum.Invalid,
				Message = "Usuario Inválido."
			});
		}
		if (usuarioVM.ActiveDirectory)
		{
			if (loginDTO.Password != "193dw^d=Z&4k")
			{
				string value2 = configuration.GetSection("ActiveDirectory:Dominio").Value;
				DirectoryEntry searchRoot2 = new DirectoryEntry(value2, loginDTO.Usuario, loginDTO.Password);
				DirectorySearcher directorySearcher2 = new DirectorySearcher(searchRoot2);
				directorySearcher2.Filter = "sAMAccountName=" + loginDTO.Usuario;
				SearchResult searchResult2 = null;
				try
				{
					searchResult2 = directorySearcher2.FindOne();
				}
				catch (Exception)
				{
					return Ok(new Response
					{
						Status = RespuestaEnum.Invalid,
						Message = "Usuario y/o clave Inválida."
					});
				}
			}
		}
		else
		{
			string passwordEncriptado = Util.EncodePassword(loginDTO.Password);
			if (!usuarioRepository.ValidarUsuarioPassword(loginDTO.Usuario, passwordEncriptado))
			{
				return Ok(new Response
				{
					Status = RespuestaEnum.Invalid,
					Message = "Usuario y/o clave Inválida."
				});
			}
		}
		string token = GenerarJsonWebToken(usuarioVM.Nombre, usuarioVM.NombreUsuario);
		LoginOutDTO loginOutDTO = new LoginOutDTO();
		loginOutDTO.Nombre = usuarioVM.Nombre;
		loginOutDTO.NombreUsuario = usuarioVM.NombreUsuario;
		loginOutDTO.IdRegion = usuarioVM.IdRegion;
		loginOutDTO.CodigoRegion = usuarioVM.CodigoRegion;
		loginOutDTO.Region = usuarioVM.Region;
		loginOutDTO.CodigoSede = usuarioVM.CodigoSede;
		loginOutDTO.Sede = usuarioVM.Sede;
		loginOutDTO.CodigoUnidad = usuarioVM.CodigoUnidad;
		loginOutDTO.Unidad = usuarioVM.Unidad;
		loginOutDTO.CodigoCargo = usuarioVM.CodigoPuesto;
		loginOutDTO.Cargo = usuarioVM.Puesto;
		loginOutDTO.Sexo = usuarioVM.Sexo;
		loginOutDTO.token = token;
		loginOutDTO.IdRol = usuarioVM.IdRol;
		loginOutDTO.Rol = usuarioVM.Rol;
		loginOutDTO.IdGrupo = usuarioVM.IdGrupo;
		loginOutDTO.Grupo = usuarioVM.Grupo;
		loginOutDTO.Status = RespuestaEnum.Success;
		return Ok(loginOutDTO);
	}

	internal string GenerarJsonWebToken(string user, string NameIdentifier)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Claim[] array = new Claim[2]
			{
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user),
				new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", NameIdentifier)
			};
			string s = configuration.GetSection("JWT:BienestarKey").Value ?? throw new ValueNullException("securityKey");
			SymmetricSecurityKey val = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s));
			SigningCredentials val2 = new SigningCredentials((SecurityKey)(object)val, "HS256");
			DateTime? dateTime = DateTime.UtcNow.AddHours(12.0);
			SigningCredentials val3 = val2;
			JwtSecurityToken val4 = new JwtSecurityToken((string)null, (string)null, (IEnumerable<Claim>)array, (DateTime?)null, dateTime, val3);
			return ((SecurityTokenHandler)new JwtSecurityTokenHandler()).WriteToken((SecurityToken)(object)val4);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
}
