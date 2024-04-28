using System;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class PeriodoVM : AuditoriaVM
{
	public int IdPeriodo { get; set; }

	public TipoPeriodoEnum TipoPeriodo { get; set; }

	public string DescripcionTipoPeriodo { get; set; } = string.Empty;


	public DateTime FechaInicio { get; set; }

	public DateTime FechaFin { get; set; }

	public DateTime FechaCorte { get; set; }

	public DateTime? FechaInicioCorte { get; set; }

	public string NombreUsuarioCreacion { get; set; } = string.Empty;


	public EstadoPeriodoEnum EstadoPeriodo { get; set; }

	public string DescripcionEstadoPeriodo { get; set; } = string.Empty;

}
