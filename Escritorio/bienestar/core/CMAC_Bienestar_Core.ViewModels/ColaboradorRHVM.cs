using System;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class ColaboradorRHVM : AuditoriaVM
{
	public int IdColaborador { get; set; }

	public string DNI { get; set; } = string.Empty;


	public string NombreApellidos { get; set; } = string.Empty;


	public string Usuario { get; set; } = string.Empty;


	public string Sexo { get; set; } = string.Empty;


	public DateTime FechaIncorporacion { get; set; }

	public RegionVM Region { get; set; } = new RegionVM();


	public string CodigoRegion { get; set; } = string.Empty;


	public string Unidad { get; set; } = string.Empty;


	public string Sede { get; set; } = string.Empty;


	public string Puesto { get; set; } = string.Empty;

}
