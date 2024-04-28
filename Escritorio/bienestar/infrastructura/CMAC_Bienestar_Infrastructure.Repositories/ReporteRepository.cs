using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using CMAC_Bienestar_Core.Common;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Exceptions;
using CMAC_Bienestar_Core.ViewModels.Filters;
using CMAC_Bienestar_Core.ViewModels.Reportes;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Office.Interop.Excel;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class ReporteRepository : IReporteRepository
{
	private readonly IConfiguration configuration;

	private readonly UniformeDataAccess uniformeDataAccess;

	private readonly UsuarioDataAccess usuarioDataAccess;

	private readonly GrupoDataAccess grupoDataAccess;

	private readonly ReportesDataAccess reportesDataAccess;

	private readonly ExtrasDataAccess extrasDataAccess;

	private readonly RegionDataAccess regionDataAccess;

	private readonly PedidoDataAccess pedidoDataAccess;

	private readonly KardexDataAccess kardexDataAccess;

	private readonly TallaDataAccess tallaDataAccess;

	private readonly ILoggerRepository logger;

	public ReporteRepository(IConfiguration configuration, ILoggerRepository logger)
	{
		this.configuration = configuration;
		uniformeDataAccess = new UniformeDataAccess(configuration);
		usuarioDataAccess = new UsuarioDataAccess(configuration);
		grupoDataAccess = new GrupoDataAccess(configuration);
		reportesDataAccess = new ReportesDataAccess(configuration);
		extrasDataAccess = new ExtrasDataAccess(configuration);
		regionDataAccess = new RegionDataAccess(configuration);
		pedidoDataAccess = new PedidoDataAccess(configuration);
		kardexDataAccess = new KardexDataAccess(configuration);
		tallaDataAccess = new TallaDataAccess(configuration);
		this.logger = logger;
	}

	public Stream GenerarReporteDescuentos(FilterReporte filtro)
	{
		ColorConverter colorConverter = new ColorConverter();
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		List<UniformeVM> list = uniformeDataAccess.ObtenerUniformesSimple().ToList();
		List<string> list2 = new List<string>
		{
			"TRABAJADOR", "DNI", "SEDE", "UNIDAD", "APELLIDOS Y NOMBRES", "SEXO", "DE_PUES_TRAB", "FECHA DE INGRESO", "GRUPO", "TOTAL DESCUENTO",
			"cuotas sugeridas", "TOTAL PRENDAS"
		};
		foreach (UniformeVM item in list)
		{
			list2.Add(item.Nombre);
			list2.Add("CANT");
		}
		list2.Add("SOL / ASIG");
		list2.Add("ACTIVO / CESADO");
		int count = list2.Count;
		string cell = "A1:" + Util.GetExcelColumnName(count) + "1";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Merge(Type.Missing);
		string text = "REPORTE DE SOLICITANTUDES DE DESCUENTOS";
		text = ((!filtro.isStock) ? (text + " POR PLANILLA") : (text + " POR STOCK"));
		if (filtro.FechaInicio.HasValue && !filtro.FechaFin.HasValue)
		{
			text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy");
		}
		if (!filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text = text + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).set_Value(Type.Missing, (object)text);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Size = 12;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num = 2;
		int num2 = 1;
		Microsoft.Office.Interop.Excel.Range range = (dynamic)worksheet.Cells[num, num2];
		foreach (string item2 in list2)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item2);
			num2++;
		}
		string cell2 = "A2:" + Util.GetExcelColumnName(count) + "2";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num3 = 3;
		int num4 = 1;
		int num5 = 0;
		List<ReporteDsctPlanillaVM> list3 = reportesDataAccess.GenerarReporteDescuentos(filtro).ToList();
		List<ReporteDsctPlanillaUniformeVM> source = reportesDataAccess.GenerarReporteDescuentosUniformes(filtro).ToList();
		foreach (ReporteDsctPlanillaVM trabajador in list3)
		{
			string cell3 = "A" + num3 + ":" + Util.GetExcelColumnName(count) + num3;
			int num6 = 0;
			num5++;
			if (num5 % 2 == 1)
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#bdd6ee"));
			}
			else
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#deeaf6"));
			}
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Bold = false;
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Black);
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Size = 11;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.NumeroTrabajador);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Region);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Unidad);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Sede);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.ApellidosNombres);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Sexo);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.PuestoTrabajador);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.FechaIngreso);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.NombreGrupo);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Total);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.Cuotas);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)trabajador.TotalPrendas);
			num4++;
			foreach (UniformeVM uniforme in list)
			{
				ReporteDsctPlanillaUniformeVM reporteDsctPlanillaUniformeVM = (from p in source
					where p.IdGrupo == trabajador.IdGrupo
					select p into u
					where u.IdUniforme == uniforme.IdUniforme
					select u into p
					where p.UsuarioCrea == trabajador.Usuario
					select p).FirstOrDefault();
				ReporteDsctPlanillaUniformeVM reporteDsctPlanillaUniformeVM2 = ((reporteDsctPlanillaUniformeVM != null) ? reporteDsctPlanillaUniformeVM : null);
				if (reporteDsctPlanillaUniformeVM2 != null)
				{
					num6 = reporteDsctPlanillaUniformeVM2.Solicitado;
				}
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)((reporteDsctPlanillaUniformeVM2 != null) ? reporteDsctPlanillaUniformeVM2.Talla : ""));
				num4++;
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)(reporteDsctPlanillaUniformeVM2?.Cantidad ?? 0));
				num4++;
			}
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)((num6 == 1) ? "SOLICITADO" : "ASIGNADO"));
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)((trabajador.Estado == 1) ? "ACTIVO" : "INACTIVO"));
			num4++;
			num3++;
			num4 = 1;
		}
		string text2 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		text2 = text2 + "\\ReporteSolicitudesDescuentoPorPlanilla_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx";
		workbook.SaveAs(text2, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text2));
	}

	public Stream GenerarReporteGrupos(FilterReporte filtro)
	{
		ColorConverter colorConverter = new ColorConverter();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		try
		{
			application.Visible = false;
			application.DisplayAlerts = false;
			workbook = application.Workbooks.Add(Type.Missing);
			worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
			worksheet.Visible = XlSheetVisibility.xlSheetVisible;
			worksheet.Activate();
			List<string> list = new List<string> { "GRUPO", "REGION", "SEDE", "Total prendas general", "ASIGNADO FEMENINO", "SOLICITADO FEMENINO", "ASIGNADO MASCULINO", "SOLICITADO MASCULINO", "Total solicitantes" };
			int count = list.Count;
			string cell = "A1:" + Util.GetExcelColumnName(count) + "1";
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Merge(Type.Missing);
			string text = "RESUMEN POR GRUPOS";
			text = ((!filtro.isStock) ? (text + " POR PLANILLA") : (text + " POR STOCK"));
			if (filtro.FechaInicio.HasValue && !filtro.FechaFin.HasValue)
			{
				text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy");
			}
			if (!filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
			{
				text = text + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
			}
			if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
			{
				text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
			}
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).set_Value(Type.Missing, (object)text);
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Size = 12;
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
			int num6 = 2;
			int num7 = 1;
			Microsoft.Office.Interop.Excel.Range range = (dynamic)worksheet.Cells[num6, num7];
			foreach (string item in list)
			{
				range = (dynamic)worksheet.Cells[num6, num7];
				range.set_Value(Type.Missing, (object)item);
				num7++;
			}
			string cell2 = "A2:" + Util.GetExcelColumnName(count) + "2";
			((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
			((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
			((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
			int num8 = 3;
			int num9 = 1;
			List<ReporteGruposVM> list2 = reportesDataAccess.GenerarReporteGrupos(filtro).ToList();
			List<ReporteGruposVM> source = reportesDataAccess.GenerarReporteGruposCount(filtro).ToList();
			foreach (ReporteGruposVM grupo in list2)
			{
				int num10 = 0;
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)grupo.Grupo);
				num9++;
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)grupo.Region);
				num9++;
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)grupo.Sede);
				num9++;
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)grupo.TotalPrendas);
				num9++;
				ReporteGruposVM reporteGruposVM = (from c in source
					where c.IdGrupo == grupo.IdGrupo
					where c.Sede == grupo.CodigoSede
					where c.Puesto == grupo.Puesto
					where c.Genero == "F"
					where c.Solicitado == 0
					select c).FirstOrDefault();
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)(reporteGruposVM?.Existencias ?? 0));
				num += reporteGruposVM?.Existencias ?? 0;
				num10 += reporteGruposVM?.Existencias ?? 0;
				num9++;
				ReporteGruposVM reporteGruposVM2 = (from c in source
					where c.IdGrupo == grupo.IdGrupo
					where c.Sede == grupo.CodigoSede
					where c.Puesto == grupo.Puesto
					where c.Genero == "F"
					where c.Solicitado == 1
					select c).FirstOrDefault();
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)(reporteGruposVM2?.Existencias ?? 0));
				num2 += reporteGruposVM2?.Existencias ?? 0;
				num10 += reporteGruposVM2?.Existencias ?? 0;
				num9++;
				ReporteGruposVM reporteGruposVM3 = (from c in source
					where c.IdGrupo == grupo.IdGrupo
					where c.Sede == grupo.CodigoSede
					where c.Puesto == grupo.Puesto
					where c.Genero == "M"
					where c.Solicitado == 0
					select c).FirstOrDefault();
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)(reporteGruposVM3?.Existencias ?? 0));
				num3 += reporteGruposVM3?.Existencias ?? 0;
				num10 += reporteGruposVM3?.Existencias ?? 0;
				num9++;
				ReporteGruposVM reporteGruposVM4 = (from c in source
					where c.IdGrupo == grupo.IdGrupo
					where c.Sede == grupo.CodigoSede
					where c.Puesto == grupo.Puesto
					where c.Genero == "M"
					where c.Solicitado == 1
					select c).FirstOrDefault();
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)(reporteGruposVM4?.Existencias ?? 0));
				num4 += reporteGruposVM4?.Existencias ?? 0;
				num10 += reporteGruposVM4?.Existencias ?? 0;
				num9++;
				range = (dynamic)worksheet.Cells[num8, num9];
				range.set_Value(Type.Missing, (object)num10);
				num5 += num10;
				num9 = 1;
				num8++;
			}
			int num11 = list2.ToList().Count + 3;
			int num12 = 1;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)"Total general");
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)num);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)num2);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)num3);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)num4);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			range = (dynamic)worksheet.Cells[num11, num12];
			range.set_Value(Type.Missing, (object)num5);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
			range.Font.Color = ColorTranslator.ToOle(Color.White);
			range.Font.Bold = true;
			num12++;
			string text2 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
			text2 = text2 + "\\ReporteGrupos_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx";
			workbook.SaveAs(text2, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
			workbook.Close(Type.Missing, Type.Missing, Type.Missing);
			application.Quit();
			logger.LogInformation("Reporte de grupos generado.");
			return new MemoryStream(File.ReadAllBytes(text2));
		}
		catch (Exception ex)
		{
			logger.LogError("Metodo: " + MethodBase.GetCurrentMethod().Name + " - " + ex.Message, new Exception(ex.Message));
			throw new Exception(ex.Message);
		}
	}

	public Stream GenerarReportePlanillaSolicitantes(FilterReporteSolicitudesCM filtroReporte, bool esStock)
	{
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		ColorConverter colorConverter = new ColorConverter();
		string[] array = new string[11]
		{
			"TRABAJADOR", "DNI", "REGIÓN", "SEDE", "UNIDAD", "APELLIDOS Y NOMBRES", "SEXO", "DE_PUES_TRAB", "FECHA DE INGRESO", "GRUPO",
			"TOTAL PRENDAS"
		};
		ICollection<UniformeVM> collection = uniformeDataAccess.ObtenerUniformesSimple();
		string[] array2 = new string[2] { "SOL / ASIG", "ACTIVO / CESADO" };
		int num = 2;
		Microsoft.Office.Interop.Excel.Range range = ((_Worksheet)worksheet).get_Range((object)"A2", Type.Missing);
		int num2 = 1;
		string[] array3 = array;
		foreach (string text in array3)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)text);
			num2++;
		}
		int num3 = 0;
		foreach (UniformeVM item in collection)
		{
			num3++;
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Nombre);
			range = (dynamic)worksheet.Cells[num, num2 + 1];
			range.set_Value(Type.Missing, (object)("CANT." + num3));
			num2 += 2;
		}
		string[] array4 = array2;
		foreach (string text2 in array4)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)text2);
			num2++;
		}
		int columnNumber = num2 - 1;
		string cell = "A2:" + Util.GetExcelColumnName(columnNumber) + "2";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		num = 1;
		string cell2 = "A1:" + Util.GetExcelColumnName(columnNumber) + "1";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		string text3 = "REPORTE DE PLANILLA DE SOLICITANTES" + (esStock ? " STOCK" : "");
		if (filtroReporte.FechaInicio.HasValue && !filtroReporte.FechaFin.HasValue)
		{
			text3 = text3 + " DESDE EL " + filtroReporte.FechaInicio.Value.ToString("dd/MM/yyyy");
		}
		if (!filtroReporte.FechaInicio.HasValue && filtroReporte.FechaFin.HasValue)
		{
			text3 = text3 + " HASTA EL " + filtroReporte.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		if (filtroReporte.FechaInicio.HasValue && filtroReporte.FechaFin.HasValue)
		{
			text3 = text3 + " DESDE EL " + filtroReporte.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtroReporte.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)text3);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Size = 12;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num4 = 3;
		num = num4;
		ICollection<ReportePlantillaSolicitantesVM> collection2 = reportesDataAccess.ObtenerUsuariosReportePlantillaSolicitantes(filtroReporte, esStock);
		string text4 = "";
		int num5 = array.Count() + 1;
		int num6 = num5;
		int num7 = array.Count();
		foreach (ReportePlantillaSolicitantesVM usuario in collection2)
		{
			DateTime? dateTime = filtroReporte.FechaIngreso;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			if (!(usuario.FechaIncorporacion <= dateTime))
			{
				continue;
			}
			text4 = "A" + num + ":" + Util.GetExcelColumnName(columnNumber) + num;
			if (num % 2 == 1)
			{
				((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#bdd6ee"));
			}
			else
			{
				((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#deeaf6"));
			}
			((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Font.Bold = false;
			((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Black);
			((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Font.Size = 11;
			range = (dynamic)worksheet.Cells[num, 1];
			range.set_Value(Type.Missing, (object)usuario.employee_id);
			range = (dynamic)worksheet.Cells[num, 2];
			range.set_Value(Type.Missing, (object)usuario.Dni);
			range = (dynamic)worksheet.Cells[num, 3];
			range.set_Value(Type.Missing, (object)usuario.Region);
			range = (dynamic)worksheet.Cells[num, 4];
			range.set_Value(Type.Missing, (object)usuario.Sede);
			range = (dynamic)worksheet.Cells[num, 5];
			range.set_Value(Type.Missing, (object)usuario.Unidad);
			range = (dynamic)worksheet.Cells[num, 6];
			range.set_Value(Type.Missing, (object)usuario.Nombre);
			range = (dynamic)worksheet.Cells[num, 7];
			range.set_Value(Type.Missing, (object)usuario.Sexo);
			range = (dynamic)worksheet.Cells[num, 8];
			range.set_Value(Type.Missing, (object)usuario.Puesto);
			range = (dynamic)worksheet.Cells[num, 9];
			range.HorizontalAlignment = XlHAlign.xlHAlignRight;
			range.set_Value(Type.Missing, (object)(usuario.FechaIncorporacion.HasValue ? usuario.FechaIncorporacion.Value.ToString("dd/MM/yyyy") : ""));
			range = (dynamic)worksheet.Cells[num, 10];
			range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
			range.set_Value(Type.Missing, (object)usuario.Grupo);
			int num8 = 0;
			num6 = num5;
			FilterPedidoItem filterPedidoItem = new FilterPedidoItem();
			filterPedidoItem.NombreUsuario = usuario.NombreUsuario;
			filterPedidoItem.FechaInicio = filtroReporte.FechaInicio;
			filterPedidoItem.FechaFin = filtroReporte.FechaFin;
			ICollection<PedidoItemVM> source = pedidoDataAccess.ObtenerTodosPedidosItemsPorUsuario(filterPedidoItem);
			source = source.Where((PedidoItemVM x) => x.TipoPedido == (TipoPedidoEnum)((!esStock) ? 1 : 3)).ToList();
			foreach (UniformeVM uniforme in collection)
			{
				PedidoItemVM pedidoItemVM = source.Where((PedidoItemVM x) => x.IdPedido == usuario.IdPedido && x.IdUniforme == uniforme.IdUniforme).FirstOrDefault();
				if (pedidoItemVM != null)
				{
					range = (dynamic)worksheet.Cells[num, num6];
					range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
					range.set_Value(Type.Missing, (object)pedidoItemVM.Talla);
					range = (dynamic)worksheet.Cells[num, num6 + 1];
					range.HorizontalAlignment = XlHAlign.xlHAlignRight;
					range.set_Value(Type.Missing, (object)pedidoItemVM.Cantidad.ToString());
					num8 += pedidoItemVM.Cantidad;
				}
				num6 += 2;
			}
			range = (dynamic)worksheet.Cells[num, num7];
			range.set_Value(Type.Missing, (object)num8.ToString());
			int num9 = num6;
			range = (dynamic)worksheet.Cells[num, num9];
			PedidoItemVM pedidoItemVM2 = source.Where((PedidoItemVM x) => x.IdPedido == usuario.IdPedido).FirstOrDefault();
			if (pedidoItemVM2 != null)
			{
				bool solicitado = pedidoItemVM2.Solicitado;
				range.set_Value(Type.Missing, (object)(solicitado ? "SOLICITADO" : "ASIGNADO"));
			}
			else
			{
				range.set_Value(Type.Missing, (object)"");
			}
			range = (dynamic)worksheet.Cells[num, num9 + 1];
			range.set_Value(Type.Missing, (object)(usuario.Estado ? "ACTIVO" : "CESADO"));
			num++;
		}
		text4 = "A" + num + ":" + Util.GetExcelColumnName(columnNumber) + num;
		((_Worksheet)worksheet).get_Range((object)text4, Type.Missing).Interior.Color = ColorTranslator.ToOle(Color.White);
		range = (dynamic)worksheet.Cells[num, 1];
		range.set_Value(Type.Missing, (object)"Total");
		range = (dynamic)worksheet.Cells[num, num7];
		if (num > num4)
		{
			range.Formula = "=SUM(" + Util.GetExcelColumnName(num7) + "3:" + Util.GetExcelColumnName(num7) + (num - 1) + ")";
		}
		else
		{
			range.set_Value(Type.Missing, (object)"0");
		}
		num6 = num5;
		foreach (UniformeVM item2 in collection)
		{
			range = (dynamic)worksheet.Cells[num, num6 + 1];
			if (num > num4)
			{
				range.Formula = "=SUM(" + Util.GetExcelColumnName(num6 + 1) + "3:" + Util.GetExcelColumnName(num6 + 1) + (num - 1) + ")";
			}
			else
			{
				range.set_Value(Type.Missing, (object)"0");
			}
			num6 += 2;
		}
		((_Worksheet)worksheet).get_Range((object)"A2", (object)"Z10000").Columns.AutoFit();
		((_Worksheet)worksheet).get_Range((object)"A2", (object)"Z10000").Rows.AutoFit();
		string text5 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		string text6 = "Reporte_Planilla_Solicitantes_" + (esStock ? "Stock_" : "") + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmm") + ".xlsx";
		workbook.SaveAs(text5 + "\\" + text6, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text5 + "\\" + text6));
	}

	public Stream GenerarReporteSolicitudes(FilterReporteSolicitudesCM filtroReporte, bool esStock)
	{
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		ColorConverter colorConverter = new ColorConverter();
		string[] array = new string[14]
		{
			"TRABAJADOR", "DNI", "REGIÓN", "SEDE", "UNIDAD", "APELLIDOS Y NOMBRES", "SEXO", "DE_PUES_TRAB", "FECHA DE INGRESO", "GRUPO",
			"PRENDAS SOLICITADAS", "PRENDAS SOLICITADAS BAJO DESCUENTO", "TOTAL PRENDAS GENERAL", "ESTADO"
		};
		int num = 2;
		Microsoft.Office.Interop.Excel.Range range = ((_Worksheet)worksheet).get_Range((object)"A2", Type.Missing);
		int num2 = 1;
		string[] array2 = array;
		foreach (string text in array2)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)text);
			num2++;
		}
		int columnNumber = num2 - 1;
		string cell = "A2:" + Util.GetExcelColumnName(columnNumber) + "2";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		num = 1;
		string cell2 = "A1:" + Util.GetExcelColumnName(columnNumber) + "1";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		string text2 = "REPORTE DE SOLICITUDES" + (esStock ? " STOCK" : "");
		if (filtroReporte.FechaInicio.HasValue && !filtroReporte.FechaFin.HasValue)
		{
			text2 = text2 + " DESDE EL " + filtroReporte.FechaInicio.Value.ToString("dd/MM/yyyy");
		}
		if (!filtroReporte.FechaInicio.HasValue && filtroReporte.FechaFin.HasValue)
		{
			text2 = text2 + " HASTA EL " + filtroReporte.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		if (filtroReporte.FechaInicio.HasValue && filtroReporte.FechaFin.HasValue)
		{
			text2 = text2 + " DESDE EL " + filtroReporte.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtroReporte.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)text2);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Size = 12;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num3 = 3;
		num = num3;
		ICollection<ReporteSolicitudesVM> collection = reportesDataAccess.ObtenerUsuariosReporteSolicitudes(filtroReporte, esStock);
		string text3 = "";
		int num4 = 12;
		int num5 = num4;
		foreach (ReporteSolicitudesVM usuario in collection)
		{
			DateTime? dateTime = filtroReporte.FechaIngreso;
			dateTime = (dateTime.HasValue ? dateTime.Value.AddDays(1.0) : new DateTime(2099, 12, 31));
			if (!(usuario.FechaIncorporacion <= dateTime))
			{
				continue;
			}
			text3 = "A" + num + ":" + Util.GetExcelColumnName(columnNumber) + num;
			if (num % 2 == 1)
			{
				((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#bdd6ee"));
			}
			else
			{
				((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#deeaf6"));
			}
			((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Font.Bold = false;
			((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Black);
			((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Font.Size = 11;
			range = (dynamic)worksheet.Cells[num, 1];
			range.set_Value(Type.Missing, (object)usuario.employee_id);
			range = (dynamic)worksheet.Cells[num, 2];
			range.set_Value(Type.Missing, (object)usuario.Dni);
			range = (dynamic)worksheet.Cells[num, 3];
			range.set_Value(Type.Missing, (object)usuario.Region);
			range = (dynamic)worksheet.Cells[num, 4];
			range.set_Value(Type.Missing, (object)usuario.Sede);
			range = (dynamic)worksheet.Cells[num, 5];
			range.set_Value(Type.Missing, (object)usuario.Unidad);
			range = (dynamic)worksheet.Cells[num, 6];
			range.set_Value(Type.Missing, (object)usuario.Nombre);
			range = (dynamic)worksheet.Cells[num, 7];
			range.set_Value(Type.Missing, (object)usuario.Sexo);
			range = (dynamic)worksheet.Cells[num, 8];
			range.set_Value(Type.Missing, (object)usuario.Puesto);
			range = (dynamic)worksheet.Cells[num, 9];
			range.HorizontalAlignment = XlHAlign.xlHAlignRight;
			range.set_Value(Type.Missing, (object)(usuario.FechaIncorporacion.HasValue ? usuario.FechaIncorporacion.Value.ToString("dd/MM/yyyy") : ""));
			range = (dynamic)worksheet.Cells[num, 10];
			range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
			range.set_Value(Type.Missing, (object)usuario.Grupo);
			FilterPedidoItem filterPedidoItem = new FilterPedidoItem();
			filterPedidoItem.NombreUsuario = usuario.NombreUsuario;
			filterPedidoItem.FechaInicio = filtroReporte.FechaInicio;
			filterPedidoItem.FechaFin = filtroReporte.FechaFin;
			ICollection<PedidoItemVM> source = pedidoDataAccess.ObtenerTodosPedidosItemsPorUsuario(filterPedidoItem);
			IEnumerable<PedidoItemVM> enumerable = source.Where((PedidoItemVM x) => x.TipoPedido == (TipoPedidoEnum)((!esStock) ? 1 : 3));
			IEnumerable<PedidoItemVM> enumerable2 = source.Where((PedidoItemVM x) => x.UsuarioCrea == usuario.NombreUsuario && x.TipoPedido == (TipoPedidoEnum)(esStock ? 4 : 2));
			int num6 = 0;
			int num7 = 0;
			foreach (PedidoItemVM item in enumerable)
			{
				num6 += item.Cantidad;
			}
			foreach (PedidoItemVM item2 in enumerable2)
			{
				num7 += item2.Cantidad;
			}
			range = (dynamic)worksheet.Cells[num, 11];
			range.set_Value(Type.Missing, (object)num6.ToString());
			range = (dynamic)worksheet.Cells[num, 12];
			range.set_Value(Type.Missing, (object)num7.ToString());
			range = (dynamic)worksheet.Cells[num, 13];
			range.Formula = "=SUM(" + Util.GetExcelColumnName(11) + num + ":" + Util.GetExcelColumnName(12) + num + ")";
			range = (dynamic)worksheet.Cells[num, 14];
			if (enumerable.Count() == 0 && enumerable2.Count() == 0)
			{
				range.set_Value(Type.Missing, (object)"");
			}
			else
			{
				bool flag = true;
				if (enumerable.Count() > 0)
				{
					flag = enumerable.First().Solicitado;
				}
				else if (enumerable2.Count() > 0)
				{
					flag = enumerable2.First().Solicitado;
				}
				range.set_Value(Type.Missing, (object)(flag ? "SOLICITADO" : "ASIGNADO"));
			}
			num++;
		}
		text3 = "A" + num + ":" + Util.GetExcelColumnName(columnNumber) + num;
		((_Worksheet)worksheet).get_Range((object)text3, Type.Missing).Interior.Color = ColorTranslator.ToOle(Color.White);
		range = (dynamic)worksheet.Cells[num, 1];
		range.set_Value(Type.Missing, (object)"Total");
		range = (dynamic)worksheet.Cells[num, 11];
		if (num > 3)
		{
			range.Formula = "=SUM(" + Util.GetExcelColumnName(11) + "3:" + Util.GetExcelColumnName(11) + (num - 1) + ")";
		}
		else
		{
			range.set_Value(Type.Missing, (object)"0");
		}
		range = (dynamic)worksheet.Cells[num, 12];
		if (collection.Count > 0)
		{
			range.Formula = "=SUM(" + Util.GetExcelColumnName(12) + "3:" + Util.GetExcelColumnName(12) + (num - 1) + ")";
		}
		else
		{
			range.set_Value(Type.Missing, (object)"0");
		}
		range = (dynamic)worksheet.Cells[num, 13];
		if (num > 3)
		{
			range.Formula = "=SUM(" + Util.GetExcelColumnName(13) + "3:" + Util.GetExcelColumnName(13) + (num - 1) + ")";
		}
		else
		{
			range.set_Value(Type.Missing, (object)"0");
		}
		((_Worksheet)worksheet).get_Range((object)"A2", (object)"Z10000").Columns.AutoFit();
		((_Worksheet)worksheet).get_Range((object)"A2", (object)"Z10000").Rows.AutoFit();
		string text4 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		string text5 = "Reporte_Solicitudes_" + (esStock ? "Stock_" : "") + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmm") + ".xlsx";
		workbook.SaveAs(text4 + "\\" + text5, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text4 + "\\" + text5));
	}

	public Stream GenerarReporteUniformes(FilterReporte filtroReporteUniformes)
	{
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		ColorConverter colorConverter = new ColorConverter();
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		string cell = "A1:E1";
		string text = "";
		text = (filtroReporteUniformes.isStock ? "REPORTE DE PRENDAS DE STOCK DESDE val1 HASTA val2" : "REPORTE DE PRENDAS DESDE val1 HASTA val2");
		string newValue = filtroReporteUniformes.FechaInicio?.ToString("dd-MM-yyyy") ?? "__";
		string newValue2 = filtroReporteUniformes.FechaFin?.ToString("dd-MM-yyyy") ?? "__";
		text = text.Replace("val1", newValue);
		text = text.Replace("val2", newValue2);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Merge(Type.Missing);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).set_Value(Type.Missing, (object)text);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).EntireColumn.AutoFit();
		string cell2 = "A2:A3";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"Prendas");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		cell2 = "B2:B3";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"Tallas");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).EntireColumn.AutoFit();
		cell2 = "C2:D2";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"Sexo/Cantidad");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		cell2 = "C3";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"F");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Blue);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#f4cccc"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		cell2 = "D3";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"M");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Blue);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#c9daf8"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		cell2 = "E2:E3";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Merge(Type.Missing);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).set_Value(Type.Missing, (object)"TOTAL DE PRENDAS");
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).VerticalAlignment = XlVAlign.xlVAlignCenter;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).EntireColumn.AutoFit();
		int num = 4;
		int num2 = 1;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		Microsoft.Office.Interop.Excel.Range range = (dynamic)worksheet.Cells[num, num2];
		ICollection<ReporteUniformesVM> collection = (filtroReporteUniformes.isStock ? reportesDataAccess.GenerarReporteUniformsStock(filtroReporteUniformes) : reportesDataAccess.GenerarReporteUniforms(filtroReporteUniformes));
		foreach (ReporteUniformesVM item in collection)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Uniforme);
			num2++;
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Talla.ToString());
			num2++;
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Femenino);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#f4cccc"));
			range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
			num2++;
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Masculino);
			range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#c9daf8"));
			range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
			num2++;
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item.Cantidad);
			num2++;
			num3 += item.Femenino;
			num4 += item.Masculino;
			num5 += item.Cantidad;
			num2 = 1;
			num++;
		}
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Merge();
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Value = "TOTAL: ";
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Interior.Color = true;
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Font.Bold = true;
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Font.Color = ColorTranslator.ToOle(Color.White);
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
		worksheet.Range[(dynamic)worksheet.Cells[num, num2], (dynamic)worksheet.Cells[num, num2 + 1]].VerticalAlignment = XlVAlign.xlVAlignCenter;
		num2 += 2;
		range = (dynamic)worksheet.Cells[num, num2];
		range.set_Value(Type.Missing, (object)num3);
		range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#f4cccc"));
		range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
		num2++;
		range = (dynamic)worksheet.Cells[num, num2];
		range.set_Value(Type.Missing, (object)num4);
		range.Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#c9daf8"));
		range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
		num2++;
		range = (dynamic)worksheet.Cells[num, num2];
		range.set_Value(Type.Missing, (object)num5);
		((dynamic)worksheet.Columns[1, Type.Missing]).AutoFit();
		((dynamic)worksheet.Columns[2, Type.Missing]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		((dynamic)worksheet.Columns[2, Type.Missing]).VerticalAlignment = XlVAlign.xlVAlignCenter;
		Microsoft.Office.Interop.Excel.Range usedRange = worksheet.UsedRange;
		Borders borders = usedRange.Borders;
		borders.LineStyle = XlLineStyle.xlContinuous;
		borders.Weight = XlBorderWeight.xlThin;
		string text2 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		string text3 = "Reporte_Uniformes_" + Convert.ToDateTime(DateTime.Now).ToString("yyyyMMddHHmm") + ".xlsx";
		workbook.SaveAs(text2 + "\\" + text3, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text2 + "\\" + text3));
	}

	public Stream GenerarReporteRanza(FilterReporte filtro)
	{
		ColorConverter colorConverter = new ColorConverter();
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		List<string> list = new List<string>
		{
			"Descripción Centro de Costo", "Centro de Costo", "SKU Artículo", "TAB", "TAB", "TAB", "TAB", "Cantidad", "TAB", "TAB",
			"TAB", "SUBINVENTARIO", "TAB", "TAB", "Cuenta", "TAB", "Dirección", "TAB", "Motivo", "TAB",
			"Referencia", "*DN"
		};
		int count = list.Count;
		string cell = "A1:" + Util.GetExcelColumnName(count) + "1";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Merge(Type.Missing);
		string text = "PLANTILLA DE PEDIDO DE MOVIMIENTO";
		text = ((!filtro.isStock) ? (text + " POR PLANILLA") : (text + " POR STOCK"));
		if (filtro.FechaInicio.HasValue && !filtro.FechaFin.HasValue)
		{
			text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy");
		}
		if (!filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text = text + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text = text + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).set_Value(Type.Missing, (object)text);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Size = 12;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num = 2;
		int num2 = 1;
		Microsoft.Office.Interop.Excel.Range range = (dynamic)worksheet.Cells[num, num2];
		foreach (string item in list)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item);
			num2++;
		}
		string cell2 = "A2:" + Util.GetExcelColumnName(count) + "2";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num3 = 3;
		int num4 = 1;
		int num5 = 0;
		List<ReporteRanzaVM> list2 = reportesDataAccess.GenerarReporteRanza(filtro).ToList();
		foreach (ReporteRanzaVM item2 in list2)
		{
			num5++;
			string cell3 = "A" + num3 + ":" + Util.GetExcelColumnName(count) + num3;
			if (num5 % 2 == 1)
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#bdd6ee"));
			}
			else
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#deeaf6"));
			}
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Bold = false;
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Black);
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Size = 11;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item2.CentroCosto);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item2.CodCentroCosto);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item2.SKU);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item2.Cantidad);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"ADQUISICIO");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item2.Cuenta);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"SALIDAS POR CONSUMO");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"TAB");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"SOLO USO ALMACEN");
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)"*DN");
			num4 = 1;
			num3++;
		}
		string text2 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		text2 = text2 + "\\ReporteRanza_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx";
		workbook.SaveAs(text2, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text2));
	}

	public Stream GenerarReporteKardex(FilterKardexVM filtro)
	{
		ColorConverter colorConverter = new ColorConverter();
		Application application = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
		Workbook workbook = null;
		Worksheet worksheet = null;
		application.Visible = false;
		application.DisplayAlerts = false;
		workbook = application.Workbooks.Add(Type.Missing);
		worksheet = (Worksheet)(dynamic)workbook.Worksheets[1];
		worksheet.Visible = XlSheetVisibility.xlSheetVisible;
		worksheet.Activate();
		int value = 0;
		ICollection<KardexVM> collection = kardexDataAccess.ObtenerKardexPorUniformeTalla(filtro);
		foreach (KardexVM item in collection.Reverse())
		{
			item.CantidadInicial = value;
			if (item.TipoRegistro == TipoRegistroKardexEnum.Entrada)
			{
				item.CantidadFinal = item.CantidadInicial + item.Valor;
			}
			else
			{
				item.CantidadFinal = item.CantidadInicial - item.Valor;
			}
			value = item.CantidadFinal.Value;
		}
		UniformeVM uniformeVM = uniformeDataAccess.ObtenerUniformePorId(filtro.IdUniforme);
		TallaVM tallaVM = tallaDataAccess.ObtenerTallaPorId(filtro.IdTalla);
		string text = ((uniformeVM != null) ? uniformeVM.Nombre : "");
		string text2 = ((tallaVM != null) ? tallaVM.NombreTalla : "");
		List<string> list = new List<string> { "Fecha Creación", "Fecha", "Usuario", "Concepto", "Inventario Inicial", "Entrada", "Salida", "Inventario Final" };
		int count = list.Count;
		string cell = "A1:" + Util.GetExcelColumnName(count) + "1";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Merge(Type.Missing);
		string text3 = "REPORTE KARDEX DE LA PRENDA " + text + " TALLA " + text2;
		if (filtro.FechaInicio.HasValue && !filtro.FechaFin.HasValue)
		{
			text3 = text3 + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy");
		}
		if (!filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text3 = text3 + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		if (filtro.FechaInicio.HasValue && filtro.FechaFin.HasValue)
		{
			text3 = text3 + " DESDE EL " + filtro.FechaInicio.Value.ToString("dd/MM/yyyy") + " HASTA EL " + filtro.FechaFin.Value.ToString("dd/MM/yyyy");
		}
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).set_Value(Type.Missing, (object)text3);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Size = 12;
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#1e4e79"));
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num = 2;
		int num2 = 1;
		Microsoft.Office.Interop.Excel.Range range = (dynamic)worksheet.Cells[num, num2];
		foreach (string item2 in list)
		{
			range = (dynamic)worksheet.Cells[num, num2];
			range.set_Value(Type.Missing, (object)item2);
			num2++;
		}
		string cell2 = "A2:" + Util.GetExcelColumnName(count) + "2";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#5b9bd5"));
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Bold = true;
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.White);
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).Font.Name = "Calibri";
		((_Worksheet)worksheet).get_Range((object)cell2, Type.Missing).HorizontalAlignment = XlHAlign.xlHAlignCenter;
		int num3 = 3;
		int num4 = 1;
		int num5 = 0;
		foreach (KardexVM item3 in collection)
		{
			num5++;
			string cell3 = "A" + num3 + ":" + Util.GetExcelColumnName(count) + num3;
			if (num5 % 2 == 1)
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#bdd6ee"));
			}
			else
			{
				((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Interior.Color = ColorTranslator.ToOle((Color)colorConverter.ConvertFromString("#deeaf6"));
			}
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Bold = false;
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Color = ColorTranslator.ToOle(Color.Black);
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Name = "Calibri";
			((_Worksheet)worksheet).get_Range((object)cell3, Type.Missing).Font.Size = 11;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.FechaCrea);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.FechaRegistro);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.NombreUsuarioCreacion);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.Concepto);
			num4++;
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.CantidadInicial);
			num4++;
			if (item3.TipoRegistro == TipoRegistroKardexEnum.Entrada)
			{
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)item3.Valor);
				num4++;
			}
			else
			{
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)"-");
				num4++;
			}
			if (item3.TipoRegistro == TipoRegistroKardexEnum.Salida)
			{
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)item3.Valor);
				num4++;
			}
			else
			{
				range = (dynamic)worksheet.Cells[num3, num4];
				range.set_Value(Type.Missing, (object)"-");
				num4++;
			}
			range = (dynamic)worksheet.Cells[num3, num4];
			range.set_Value(Type.Missing, (object)item3.CantidadFinal);
			num4++;
			num4 = 1;
			num3++;
		}
		string text4 = configuration.GetSection("Rutas:Reportes").Value ?? throw new ValueNullException("rutaReportes");
		text4 = text4 + "\\ReporteKardex_" + DateTime.Now.Date.Year + "_" + DateTime.Now.Date.Month + "_" + DateTime.Now.Date.Day + ".xlsx";
		workbook.SaveAs(text4, XlFileFormat.xlOpenXMLWorkbook, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlUserResolution, true, Missing.Value, Missing.Value, Missing.Value);
		workbook.Close(Type.Missing, Type.Missing, Type.Missing);
		application.Quit();
		return new MemoryStream(File.ReadAllBytes(text4));
	}
}
