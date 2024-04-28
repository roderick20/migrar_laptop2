namespace CMAC_Bienestar_WebAPI.DTOs;

public class UsuarioDTOOut
{
	public int IdUsuario { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public string Dni { get; set; } = string.Empty;


	public string Email { get; set; } = string.Empty;


	public string IdTrabajador { get; set; } = string.Empty;


	public int? IdColaborador { get; set; }

	public string employee_id { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;


	public int? IdRol { get; set; }

	public string Rol { get; set; } = string.Empty;


	public bool ActiveDirectory { get; set; }

	public string Grupo { get; set; } = string.Empty;


	public int? IdGrupo { get; set; }
}
