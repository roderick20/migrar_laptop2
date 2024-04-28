using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class UniformeKardexDTOOut
{
	public int IdUniforme { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public List<TallaKardexDTO> Tallas { get; set; } = new List<TallaKardexDTO>();

}
