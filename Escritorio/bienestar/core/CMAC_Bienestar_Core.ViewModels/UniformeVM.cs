using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class UniformeVM : AuditoriaVM
{
	public int IdUniforme { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string SKU { get; set; } = string.Empty;


	public int[]? IdsTallas { get; set; }

	public List<TallaVM> Tallas { get; set; } = new List<TallaVM>();


	public string Genero { get; set; } = string.Empty;


	public int IdTallaEstandar { get; set; }

	public string NombreTallaEstandar { get; set; } = string.Empty;


	public TallaVM TallaEstandar { get; set; } = new TallaVM();


	public string GUIDImagen { get; set; } = string.Empty;


	public string Detalle { get; set; } = string.Empty;


	public string GUIDDetalle { get; set; } = string.Empty;


	public decimal Precio { get; set; }

	public int Item { get; set; }
}
