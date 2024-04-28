using System;

namespace CMAC_Bienestar_Core.ViewModels.Filters;

public class FilterReporte
{
	public DateTime? FechaInicio { get; set; }

	public DateTime? FechaFin { get; set; }

	public bool isStock { get; set; }
}
