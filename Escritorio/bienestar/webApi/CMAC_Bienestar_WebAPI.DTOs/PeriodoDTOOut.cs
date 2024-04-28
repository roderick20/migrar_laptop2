using System;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class PeriodoDTOOut
{
	public int IdPeriodo { get; set; }

	public TipoPeriodoEnum TipoPeriodo { get; set; }

	public string DescripcionTipoPeriodo { get; set; } = string.Empty;


	public DateTime FechaInicio { get; set; }

	public DateTime FechaFin { get; set; }

	public DateTime FechaCorte { get; set; }

	public DateTime? FechaInicioCorte { get; set; }

	public EstadoPeriodoEnum EstadoPeriodo { get; set; }

	public string DescripcionEstadoPeriodo { get; set; } = string.Empty;


	public string UsuarioCrea { get; set; } = string.Empty;


	public string NombreUsuarioCreacion { get; set; } = string.Empty;


	public bool Estado { get; set; }
}
