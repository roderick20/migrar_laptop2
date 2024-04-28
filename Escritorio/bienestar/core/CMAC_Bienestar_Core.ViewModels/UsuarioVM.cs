using System;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class UsuarioVM : AuditoriaVM
{
	public int IdUsuario { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string NombreUsuario { get; set; } = string.Empty;


	public string Password { get; set; } = string.Empty;


	public bool ActiveDirectory { get; set; }

	public string Dni { get; set; } = string.Empty;


	public string Email { get; set; } = string.Empty;


	public int? IdColaborador { get; set; }

	public string employee_id { get; set; } = string.Empty;


	public int? IdRol { get; set; }

	public string Rol { get; set; } = string.Empty;


	public string CodigoUnidad { get; set; } = string.Empty;


	public string Unidad { get; set; } = string.Empty;


	public string CodigoSede { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string CodigoPuesto { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;


	public int? IdRegion { get; set; }

	public string CodigoRegion { get; set; } = string.Empty;


	public string Region { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public DateTime? FechaIncorporacion { get; set; }

	public string Grupo { get; set; } = string.Empty;


	public int? IdGrupo { get; set; }
}
