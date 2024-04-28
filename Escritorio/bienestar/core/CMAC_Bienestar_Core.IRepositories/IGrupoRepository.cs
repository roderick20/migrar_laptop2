using System.Collections.Generic;
using System.IO;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IGrupoRepository
{
	ICollection<GrupoVM> ObtenerGrupos();

	GrupoVM? ObtenerGrupoPorId(int idGrupo);

	ResponseBase AgregarGrupo(GrupoVM grupo);

	ResponseBase ActualizarGrupo(GrupoVM grupo);

	int EliminarGrupo(int idGrupo);

	string CargaMasivaGrupo(Stream streamCargaMasiva);

	string CargarDataInicialGrupos();

	string CargarDataInicialGruposVersion2();

	string CargarDataInicialGruposxUniformes();
}
