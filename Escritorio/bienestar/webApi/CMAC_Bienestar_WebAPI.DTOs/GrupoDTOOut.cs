using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class GrupoDTOOut
{
	public int IdGrupo { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string Genero { get; set; } = string.Empty;


	public bool Estado { get; set; }

	public List<GrupoEntidadDTOOut> GrupoEntidades { get; set; } = new List<GrupoEntidadDTOOut>();


	public List<UniformeGrupoDTOOut> Uniformes { get; set; } = new List<UniformeGrupoDTOOut>();

}
