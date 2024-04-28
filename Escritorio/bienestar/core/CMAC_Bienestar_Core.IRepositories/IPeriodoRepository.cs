using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IPeriodoRepository
{
	ICollection<PeriodoVM> ObtenerPeriodos();

	PeriodoVM ObtenerPeriodoPorId(int idPeriodo);

	int AgregarPeriodo(PeriodoVM periodo);

	int ActualizarPeriodo(PeriodoVM periodo);

	int EliminarPeriodo(int idPeriodo);

	int ActualizarEstadoPeriodo(PeriodoVM periodo);

	PeriodoVM ObtenerPeriodoActivo(string usuario, int tipoPedido);
}
