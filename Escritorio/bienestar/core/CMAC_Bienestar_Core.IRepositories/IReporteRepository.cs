using System.IO;
using CMAC_Bienestar_Core.ViewModels.Filters;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IReporteRepository
{
	Stream GenerarReporteDescuentos(FilterReporte filtro);

	Stream GenerarReportePlanillaSolicitantes(FilterReporteSolicitudesCM filtroReporte, bool esStock);

	Stream GenerarReporteGrupos(FilterReporte filtro);

	Stream GenerarReporteSolicitudes(FilterReporteSolicitudesCM filtroReporte, bool esStock);

	Stream GenerarReporteUniformes(FilterReporte filtroReporteUniformes);

	Stream GenerarReporteRanza(FilterReporte filtro);

	Stream GenerarReporteKardex(FilterKardexVM filtro);
}
