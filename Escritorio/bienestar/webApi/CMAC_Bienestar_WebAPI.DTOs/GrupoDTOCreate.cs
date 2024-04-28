using System.Collections.Generic;

namespace CMAC_Bienestar_WebAPI.DTOs;

public class GrupoDTOCreate
{
	public string Nombre { get; set; } = string.Empty;


	public string Genero { get; set; } = string.Empty;


	public List<UniformeGrupoDTOIn> UniformeGrupo { get; set; } = new List<UniformeGrupoDTOIn>();


	public List<GrupoEntidadDTOIn> GrupoEntidades { get; set; } = new List<GrupoEntidadDTOIn>();

}
