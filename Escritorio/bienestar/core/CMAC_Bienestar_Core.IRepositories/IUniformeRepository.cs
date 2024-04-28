using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IUniformeRepository
{
	ICollection<UniformeVM> ObtenerUniformes();

	ICollection<UniformeVM> ObtenerUniformesSimple();

	UniformeVM? ObtenerUniformePorId(int idUniforme);

	ICollection<UniformeVM> ObtenerUniformesPorGrupo(string codigoRegion, string codigoSede, string codigoUnidad, string codigoPuesto, string sexo);

	ICollection<UniformeKardexVM> ObtenerUniformesConKardex();

	int AgregarUniforme(UniformeVM uniforme);

	int ActualizarUniforme(UniformeVM uniforme);

	int EliminarUniforme(int idUniforme);

	UniformeVM? ObtenerUniformePorNombre(string Uniforme_name);
}
