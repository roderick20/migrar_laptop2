using System;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class KardexDTOOut
{
	public int IdKardex { get; set; }

	public int IdUniforme { get; set; }

	public string Uniforme { get; set; } = string.Empty;


	public int IdTalla { get; set; }

	public string Talla { get; set; } = string.Empty;


	public DateTime FechaRegistro { get; set; }

	public string Concepto { get; set; } = string.Empty;


	public TipoRegistroKardexEnum TipoRegistro { get; set; }

	public string DescripcionTipoRegistro { get; set; } = string.Empty;


	public int Valor { get; set; }

	public int CantidadInicial { get; set; }

	public int CantidadFinal { get; set; }

	public string NombreUsuarioCreacion { get; set; } = string.Empty;


	public DateTime FechaCrea { get; set; }
}
