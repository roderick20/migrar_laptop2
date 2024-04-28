using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IExtrasRepository
{
	ICollection<SedeVM> ObtenerSedes();

	ICollection<UnidadVM> ObtenerUnidades();

	ICollection<PuestoVM> ObtenerPuestos();
}
