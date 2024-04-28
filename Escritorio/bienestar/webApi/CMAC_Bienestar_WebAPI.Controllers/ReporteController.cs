using System;
using System.IO;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels.Filters;
using CMAC_Bienestar_WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class ReporteController : ControllerBase
{
	private readonly IReporteRepository reporteRepository;

	public ReporteController(IReporteRepository reporteRepository)
	{
		this.reporteRepository = reporteRepository;
	}

	[HttpPost("ReporteDescuentoPlanilla")]
	public ActionResult GenerarReporteDescuentos(FilterReporte filtro)
	{
		Stream fileStream = reporteRepository.GenerarReporteDescuentos(filtro);
		string text = "ReporteSolicitudesDescuentoPorPlanilla_";
		text = ((!filtro.isStock) ? (text + "PorPlanilla_") : (text + "PorStock_"));
		return File(fileStream, "application/octet-stream", text + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReportePlanillaSolicitantes")]
	public ActionResult GenerarReportePlanillaSolicitantes(FilterReporteSolicitudesCM filtroReporteSolicitudes)
	{
		Stream fileStream = reporteRepository.GenerarReportePlanillaSolicitantes(filtroReporteSolicitudes, esStock: false);
		return File(fileStream, "application/octet-stream", "ReportePlanillaSolicitantes_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReportePlanillaSolicitantesStock")]
	public ActionResult GenerarReportePlanillaSolicitantesStock(FilterReporteSolicitudesCM filtroReporteSolicitudes)
	{
		Stream fileStream = reporteRepository.GenerarReportePlanillaSolicitantes(filtroReporteSolicitudes, esStock: true);
		return File(fileStream, "application/octet-stream", "ReportePlanillaSolicitantesStock_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReporteGrupos")]
	public ActionResult GenerarReporteGrupos(FilterReporte filtro)
	{
		Stream fileStream = reporteRepository.GenerarReporteGrupos(filtro);
		string text = "ReporteGrupos_";
		text = ((!filtro.isStock) ? (text + "PorPlanilla_") : (text + "PorStock_"));
		return File(fileStream, "application/octet-stream", text + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReporteSolicitudes")]
	public ActionResult GenerarReporteSolicitudes(FilterReporteSolicitudesCM filtroReporteSolicitudes)
	{
		Stream fileStream = reporteRepository.GenerarReporteSolicitudes(filtroReporteSolicitudes, esStock: false);
		return File(fileStream, "application/octet-stream", "ReporteSolicitudes_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReporteSolicitudesStock")]
	public ActionResult GenerarReporteSolicitudesStock(FilterReporteSolicitudesCM filtroReporteSolicitudes)
	{
		Stream fileStream = reporteRepository.GenerarReporteSolicitudes(filtroReporteSolicitudes, esStock: true);
		return File(fileStream, "application/octet-stream", "ReporteSolicitudesStock_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReporteUniformes")]
	public ActionResult GenerarReporteUniformes(FilterReporte filtroReporteUniformes)
	{
		Stream fileStream = reporteRepository.GenerarReporteUniformes(filtroReporteUniformes);
		return File(fileStream, "application/octet-stream", "Reporte_Uniformes.xlsx");
	}

	[HttpPost("ReporteRanza")]
	public ActionResult GenerarReporteRanza(FilterReporte filtro)
	{
		Stream fileStream = reporteRepository.GenerarReporteRanza(filtro);
		string text = "ReporteRanza";
		text = ((!filtro.isStock) ? (text + "PorPlanilla_") : (text + "PorStock_"));
		return File(fileStream, "application/octet-stream", text + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}

	[HttpPost("ReporteKardex")]
	public ActionResult GenerarReporteKardex(FilterKardexVM filtro)
	{
		Stream fileStream = reporteRepository.GenerarReporteKardex(filtro);
		return File(fileStream, "application/octet-stream", "ReporteKardex" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx");
	}
}
