using System;

namespace CMAC_Bienestar_Core.ViewModels.Filters;

public class FilterKardexVM
{
	public int IdUniforme { get; set; }

	public int IdTalla { get; set; }

	public DateTime? FechaInicio { get; set; }

	public DateTime? FechaFin { get; set; }
}
