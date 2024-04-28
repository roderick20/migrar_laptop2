using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class LoginOutDTO
{
	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public int? IdRegion { get; set; }

	public string CodigoRegion { get; set; } = string.Empty;


	public string Region { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string CodigoUnidad { get; set; } = string.Empty;


	public string Unidad { get; set; } = string.Empty;


	public string CodigoCargo { get; set; } = string.Empty;


	public string Cargo { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public string token { get; set; } = string.Empty;


	public RespuestaEnum Status { get; set; }

	public string Rol { get; set; } = string.Empty;


	public int? IdRol { get; set; }

	public int? IdGrupo { get; set; }

	public string Grupo { get; set; } = string.Empty;

}
