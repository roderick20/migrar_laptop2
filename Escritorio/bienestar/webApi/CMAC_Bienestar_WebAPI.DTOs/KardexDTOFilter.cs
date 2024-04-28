using System;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class KardexDTOFilter
{
	public DateTime FechaRegistro { get; set; }

	public string Concepto { get; set; } = string.Empty;


	public TipoRegistroKardexEnum TipoRegistro { get; set; }

	public int Valor { get; set; }

	public string UsuarioModi { get; set; } = string.Empty;

}
