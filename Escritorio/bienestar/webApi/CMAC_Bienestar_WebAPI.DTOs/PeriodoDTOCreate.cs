using System;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class PeriodoDTOCreate
{
	public TipoPeriodoEnum TipoPeriodo { get; set; }

	public DateTime FechaInicio { get; set; }

	public DateTime FechaFin { get; set; }

	public DateTime FechaCorte { get; set; }

	public DateTime? FechaInicioCorte { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
