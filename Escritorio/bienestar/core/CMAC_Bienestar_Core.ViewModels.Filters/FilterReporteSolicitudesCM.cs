using System;

namespace CMAC_Bienestar_Core.ViewModels.Filters;

public class FilterReporteSolicitudesCM
{
	public DateTime? FechaInicio { get; set; }

	public DateTime? FechaFin { get; set; }

	public DateTime? FechaIngreso { get; set; }

	public bool? EstadoUsuario { get; set; }
}
