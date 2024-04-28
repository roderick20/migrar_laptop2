using System.Collections.Generic;

namespace CMAC_Bienestar_Core.ViewModels;

public class UniformeKardexVM
{
	public int IdUniforme { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public List<TallaKardexVM> Tallas { get; set; } = new List<TallaKardexVM>();

}
