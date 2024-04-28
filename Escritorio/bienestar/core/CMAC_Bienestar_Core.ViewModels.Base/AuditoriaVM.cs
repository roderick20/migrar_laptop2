using System;
using System.ComponentModel.DataAnnotations;

namespace CMAC_Bienestar_Core.ViewModels.Base;

public class AuditoriaVM
{
	[Display(Name = "Usuario Creacion")]
	[MaxLength(20)]
	public string UsuarioCrea { get; set; } = string.Empty;


	[Display(Name = "Fecha Creacion")]
	public DateTime FechaCrea { get; set; }

	[Display(Name = "Usuario Modificacion")]
	[MaxLength(20)]
	public string UsuarioModi { get; set; } = string.Empty;


	[Display(Name = "Fecha Modificacion")]
	public DateTime? FechaModi { get; set; }

	[Display(Name = "Estado")]
	public bool Estado { get; set; } = true;

}
