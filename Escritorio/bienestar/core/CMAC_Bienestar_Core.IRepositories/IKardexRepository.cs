using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Filters;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IKardexRepository
{
	ICollection<KardexVM> ObtenerKardexPorUniformeTalla(FilterKardexVM filtro);

	KardexVM ObtenerKardexPorId(int idKardex);

	KardexVM ObtenerUltimoKardex(FilterKardexVM filtro);

	int AgregarKardex(KardexVM kardex);

	void ActualizarKardex(KardexVM kardex);

	void EliminarKardex(int idKardex);
}
