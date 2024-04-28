using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class GrupoVM : AuditoriaVM
{
	public int IdGrupo { get; set; }

	public string Nombre { get; set; } = string.Empty;


	public string Genero { get; set; } = string.Empty;


	public int[]? IdsUniformes { get; set; }

	public List<UniformeVM> Uniformes { get; set; } = new List<UniformeVM>();


	public List<UniformeGrupoVM> UniformeGrupo { get; set; } = new List<UniformeGrupoVM>();


	public List<GrupoEntidadVM> GrupoEntidades { get; set; } = new List<GrupoEntidadVM>();

}
