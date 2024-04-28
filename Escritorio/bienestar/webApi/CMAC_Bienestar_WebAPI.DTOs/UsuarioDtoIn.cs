namespace CMAC_Bienestar_WebAPI.DTOs;

public class UsuarioDtoIn
{
	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public string Password { get; set; } = string.Empty;


	public string Dni { get; set; } = string.Empty;


	public string Email { get; set; } = string.Empty;


	public string employee_id { get; set; } = string.Empty;


	public int? IdRol { get; set; }

	public bool ActiveDirectory { get; set; }
}
