using System;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class KardexDTOCreate
{
	public int IdUniforme { get; set; }

	public int IdTalla { get; set; }

	public DateTime FechaRegistro { get; set; }

	public string Concepto { get; set; } = string.Empty;


	public TipoRegistroKardexEnum TipoRegistro { get; set; }

	public int Valor { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
