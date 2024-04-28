using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_Core.ViewModels.Base;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class GrupoRepository : IGrupoRepository
{
	private readonly GrupoDataAccess grupoDataAccess;

	private readonly UniformeDataAccess uniformeDataAccess;

	private readonly TallaDataAccess tallaDataAccess;

	private readonly RegionDataAccess regionDataAccess;

	private readonly ExtrasDataAccess extrasDataAccess;

	public GrupoRepository(IConfiguration configuration)
	{
		grupoDataAccess = new GrupoDataAccess(configuration);
		uniformeDataAccess = new UniformeDataAccess(configuration);
		tallaDataAccess = new TallaDataAccess(configuration);
		regionDataAccess = new RegionDataAccess(configuration);
		extrasDataAccess = new ExtrasDataAccess(configuration);
	}

	public ResponseBase ActualizarGrupo(GrupoVM grupo)
	{
		string text = "";
		int num = 0;
		int num2 = grupoDataAccess.ActualizarGrupo(grupo);
		switch (num2)
		{
		case -1:
			return new ResponseBase
			{
				Codigo = -1,
				Mensaje = "No se encontro ningun grupo con id = " + num2 + "."
			};
		case -2:
			return new ResponseBase
			{
				Codigo = -2,
				Mensaje = "Ya existe un grupo con los datos proporcionados."
			};
		default:
			grupoDataAccess.EliminarGrupoEntidad(num2);
			foreach (GrupoEntidadVM entidad in grupo.GrupoEntidades)
			{
				entidad.IdGrupo = num2;
				GrupoVM grupoVM = grupoDataAccess.ObtenerGrupoPorFiltros(entidad.CodigoRegion, entidad.CodigoSede, entidad.CodigoUnidad, entidad.CodigoPuesto, grupo.Genero);
				if (grupoVM == null)
				{
					grupoDataAccess.AgregarGrupoEntidad(entidad);
					continue;
				}
				string text2 = "Region: " + regionDataAccess.ObtenerRegionPorCodigo(entidad.CodigoRegion).Nombre;
				string text3 = "Sede: " + extrasDataAccess.ObtenerSedes().First((SedeVM r) => r.CodigoSede == entidad.CodigoRegion).DetalleSede;
				string text4 = "Unidad: " + extrasDataAccess.ObtenerUnidades().First((UnidadVM r) => r.CodigoUnidad == entidad.CodigoUnidad).DetalleUnidad;
				string text5 = "Puesto: " + entidad.CodigoPuesto;
				text = text + "La combinacion (" + text2 + ", " + text3 + ", " + text4 + ", " + text5 + ") ya existe en el grupo: " + grupoVM.Nombre + ". \n ";
				num++;
			}
			if (num == grupo.GrupoEntidades.Count)
			{
				grupoDataAccess.EliminarTotalGrupo(num2);
				text = "No se agrego el grupo. " + text;
			}
			if ((num != 0 && num != grupo.GrupoEntidades.Count) || num == 0)
			{
				text = "Se agrego el grupo con los siguientes errores. " + text;
				uniformeDataAccess.EliminarUniformesPorGrupo(num2);
				foreach (UniformeGrupoVM item in grupo.UniformeGrupo)
				{
					uniformeDataAccess.AgregarUniformesPorGrupo(item.IdUniforme, num2, item.Item);
				}
			}
			return new ResponseBase
			{
				Codigo = ((num == 0) ? num2 : (-3)),
				Mensaje = ((num == 0) ? "" : text)
			};
		}
	}

	public ResponseBase AgregarGrupo(GrupoVM grupo)
	{
		string text = "";
		int num = 0;
		int num2 = grupoDataAccess.AgregarGrupo(grupo);
		switch (num2)
		{
		case -1:
			return new ResponseBase
			{
				Codigo = -1,
				Mensaje = "No se encontro ningun grupo con id = " + num2 + "."
			};
		case -2:
			return new ResponseBase
			{
				Codigo = -2,
				Mensaje = "Ya existe un grupo con los datos proporcionados."
			};
		default:
			grupoDataAccess.EliminarGrupoEntidad(num2);
			foreach (GrupoEntidadVM entidad in grupo.GrupoEntidades)
			{
				entidad.IdGrupo = num2;
				GrupoVM grupoVM = grupoDataAccess.ObtenerGrupoPorFiltros(entidad.CodigoRegion, entidad.CodigoSede, entidad.CodigoUnidad, entidad.CodigoPuesto, grupo.Genero);
				if (grupoVM == null)
				{
					grupoDataAccess.AgregarGrupoEntidad(entidad);
					continue;
				}
				string text2 = "Region: " + regionDataAccess.ObtenerRegionPorCodigo(entidad.CodigoRegion).Nombre;
				string text3 = "Sede: " + extrasDataAccess.ObtenerSedes().First((SedeVM r) => r.CodigoSede == entidad.CodigoRegion).DetalleSede;
				string text4 = "Unidad: " + extrasDataAccess.ObtenerUnidades().First((UnidadVM r) => r.CodigoUnidad == entidad.CodigoUnidad).DetalleUnidad;
				string text5 = "Puesto: " + entidad.CodigoPuesto;
				text = text + "La combinacion (" + text2 + ", " + text3 + ", " + text4 + ", " + text5 + ") ya existe en el grupo: " + grupoVM.Nombre + ". \n ";
				num++;
			}
			if (num == grupo.GrupoEntidades.Count)
			{
				grupoDataAccess.EliminarTotalGrupo(num2);
				text = "No se agrego el grupo. " + text;
			}
			if ((num != 0 && num != grupo.GrupoEntidades.Count) || num == 0)
			{
				text = "Se agrego el grupo con los siguientes errores. " + text;
				uniformeDataAccess.EliminarUniformesPorGrupo(num2);
				foreach (UniformeGrupoVM item in grupo.UniformeGrupo)
				{
					uniformeDataAccess.AgregarUniformesPorGrupo(item.IdUniforme, num2, item.Item);
				}
			}
			return new ResponseBase
			{
				Codigo = ((num == 0) ? num2 : (-3)),
				Mensaje = ((num == 0) ? "" : text)
			};
		}
	}

	public int EliminarGrupo(int idGrupo)
	{
		return grupoDataAccess.EliminarGrupo(idGrupo);
	}

	public ICollection<GrupoVM> ObtenerGrupos()
	{
		return grupoDataAccess.ObtenerGrupos();
	}

	public GrupoVM? ObtenerGrupoPorId(int idGrupo)
	{
		return grupoDataAccess.ObtenerGrupoPorId(idGrupo);
	}

	public string CargaMasivaGrupo(Stream streamCargaMasiva)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		string text = "";
		List<GrupoVM> list = new List<GrupoVM>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(streamCargaMasiva);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress start = val2.Dimension.Start;
			ExcelCellAddress end = val2.Dimension.End;
			List<SedeVM> sedes = extrasDataAccess.ObtenerSedes().ToList();
			List<PuestoVM> puestos = extrasDataAccess.ObtenerPuestos().ToList();
			List<UnidadVM> unidades = extrasDataAccess.ObtenerUnidades().ToList();
			for (int i = 2; i <= end.Row; i++)
			{
				bool flag = true;
				string nombre = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string text2 = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string text3 = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				string text4 = ((ExcelRangeBase)val2.Cells[i, 4]).Text;
				string text5 = ((ExcelRangeBase)val2.Cells[i, 5]).Text;
				if (nombre == "")
				{
					text = text + "El campo nombre de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text2 == "")
				{
					text = text + "El campo puesto de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text3 == "")
				{
					text = text + "El campo unidad de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text4 == "")
				{
					text = text + "El campo genero de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text5 == "")
				{
					text = text + "El campo sede de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (!flag)
				{
					continue;
				}
				List<GrupoVM> list2 = (from g in grupoDataAccess.ObtenerGrupos()
					where g.Nombre == nombre
					select g).ToList();
				if (list2.Count != 0)
				{
					continue;
				}
				if (ComprobarSede(sedes, text5))
				{
					if (ComprobarPuesto(puestos, text2))
					{
						if (ComprobarUnidad(unidades, text3))
						{
							if (ComprobarGenero(text4))
							{
								list.Add(new GrupoVM
								{
									Nombre = nombre,
									Genero = text4,
									UsuarioCrea = "admin"
								});
							}
							else
							{
								text = text + "El Genero debe ser Femenino o Masculino en la fila " + i + ",";
							}
						}
						else
						{
							text = text + "No existe una Unidad con ese nombre en la fila " + i + ",";
						}
					}
					else
					{
						text = text + "No existe un Puesto con ese nombre en la fila " + i + ",";
					}
				}
				else
				{
					text = text + "No existe una SEDE con ese nombre en la fila " + i + ",";
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (text.Equals(""))
		{
			foreach (GrupoVM item in list)
			{
				grupoDataAccess.AgregarGrupo(item);
			}
		}
		return text;
	}

	public string CargarDataInicialGrupos()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		string text = "";
		Stream stream = new FileStream("CargaMasiva/DatosGrupos.xlsx", FileMode.Open, FileAccess.Read);
		List<string> list = new List<string>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(stream);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress start = val2.Dimension.Start;
			ExcelCellAddress end = val2.Dimension.End;
			List<RegionVM> source = regionDataAccess.ObtenerRegiones().ToList();
			List<SedeVM> source2 = extrasDataAccess.ObtenerSedes().ToList();
			List<PuestoVM> source3 = extrasDataAccess.ObtenerPuestos().ToList();
			List<UnidadVM> source4 = extrasDataAccess.ObtenerUnidades().ToList();
			List<RegionVM> list2 = regionDataAccess.ObtenerTodasRegionesConGrupo().ToList();
			List<SedeVM> list3 = extrasDataAccess.ObtenerTodasSedesConGrupo().ToList();
			List<UnidadVM> list4 = extrasDataAccess.ObtenerTodasUnidadesConGrupo().ToList();
			List<PuestoVM> list5 = extrasDataAccess.ObtenerTodasPuestosConGrupo().ToList();
			for (int i = 2; i <= end.Row; i++)
			{
				string nombre_region = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string nombre_sede = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string nombre_unidad = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				string nombre_puesto = ((ExcelRangeBase)val2.Cells[i, 4]).Text;
				string text2 = ((ExcelRangeBase)val2.Cells[i, 5]).Text;
				string text3 = ((ExcelRangeBase)val2.Cells[i, 6]).Text;
				GrupoVM grupo = grupoDataAccess.ObtenerGrupoPorNombre(text3);
				if (grupo == null)
				{
					grupo = new GrupoVM();
					grupo.Nombre = text3;
					grupo.Genero = ((text2 == "F") ? "Femenino" : "Masculino");
					grupo.Estado = true;
					grupo.UsuarioCrea = "admin";
					int idGrupo = grupoDataAccess.AgregarGrupo(grupo);
					grupo.IdGrupo = idGrupo;
				}
				RegionVM region = source.Where((RegionVM x) => x.Nombre == nombre_region).FirstOrDefault();
				if (region != null)
				{
					RegionVM regionVM = list2.Where((RegionVM x) => x.Codigo == region.Codigo && x.IdGrupo == grupo.IdGrupo).FirstOrDefault();
					if (regionVM == null)
					{
						regionDataAccess.AgregarRegionesPorGrupo(region.Codigo, grupo.IdGrupo);
						RegionVM regionVM2 = new RegionVM();
						regionVM2.IdGrupo = grupo.IdGrupo;
						regionVM2.Codigo = region.Codigo;
						regionVM2.Nombre = region.Nombre;
						list2.Add(regionVM2);
					}
				}
				else
				{
					string item = "No existe la región con nombre: " + nombre_region;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
				SedeVM sede = source2.Where((SedeVM x) => x.DetalleSede == nombre_sede).FirstOrDefault();
				if (sede != null)
				{
					SedeVM sedeVM = list3.Where((SedeVM x) => x.CodigoSede == sede.CodigoSede && x.IdGrupo == grupo.IdGrupo).FirstOrDefault();
					if (sedeVM == null)
					{
						extrasDataAccess.AgregarSedePorGrupo(sede.CodigoSede, grupo.IdGrupo);
						SedeVM sedeVM2 = new SedeVM();
						sedeVM2.IdGrupo = grupo.IdGrupo;
						sedeVM2.CodigoSede = sede.CodigoSede;
						sedeVM2.DetalleSede = sede.DetalleSede;
						list3.Add(sedeVM2);
					}
				}
				else
				{
					string item2 = "No existe la sede con nombre: " + nombre_sede;
					if (!list.Contains(item2))
					{
						list.Add(item2);
					}
				}
				UnidadVM unidad = source4.Where((UnidadVM x) => x.DetalleUnidad == nombre_unidad).FirstOrDefault();
				if (unidad != null)
				{
					UnidadVM unidadVM = list4.Where((UnidadVM x) => x.CodigoUnidad == unidad.CodigoUnidad && x.IdGrupo == grupo.IdGrupo).FirstOrDefault();
					if (unidadVM == null)
					{
						extrasDataAccess.AgregarUnidadPorGrupo(unidad.CodigoUnidad, grupo.IdGrupo);
						UnidadVM unidadVM2 = new UnidadVM();
						unidadVM2.IdGrupo = grupo.IdGrupo;
						unidadVM2.CodigoUnidad = unidad.CodigoUnidad;
						unidadVM2.DetalleUnidad = unidad.DetalleUnidad;
						list4.Add(unidadVM2);
					}
				}
				else
				{
					string item3 = "No existe la unidad con nombre: " + nombre_unidad;
					if (!list.Contains(item3))
					{
						list.Add(item3);
					}
				}
				PuestoVM puesto = source3.Where((PuestoVM x) => x.DetallePuesto == nombre_puesto).FirstOrDefault();
				if (puesto != null)
				{
					PuestoVM puestoVM = list5.Where((PuestoVM x) => x.CodigoPuesto == puesto.CodigoPuesto && x.IdGrupo == grupo.IdGrupo).FirstOrDefault();
					if (puestoVM == null)
					{
						extrasDataAccess.AgregarPuestoPorGrupo(puesto.CodigoPuesto, grupo.IdGrupo);
						PuestoVM puestoVM2 = new PuestoVM();
						puestoVM2.IdGrupo = grupo.IdGrupo;
						puestoVM2.CodigoPuesto = puesto.CodigoPuesto;
						puestoVM2.DetallePuesto = puesto.DetallePuesto;
						list5.Add(puestoVM2);
					}
					continue;
				}
				string item4 = "No existe el puesto con nombre: " + nombre_puesto;
				if (!list.Contains(item4))
				{
					if (text != "")
					{
						text += ". ";
					}
					list.Add(item4);
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		foreach (string item5 in list)
		{
			text += item5;
		}
		return text;
	}

	public string CargarDataInicialGruposVersion2()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		string text = "";
		Stream stream = new FileStream("CargaMasiva/DatosGrupos.xlsx", FileMode.Open, FileAccess.Read);
		List<string> list = new List<string>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(stream);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress start = val2.Dimension.Start;
			ExcelCellAddress end = val2.Dimension.End;
			List<RegionVM> source = regionDataAccess.ObtenerRegiones().ToList();
			List<SedeVM> source2 = extrasDataAccess.ObtenerSedes().ToList();
			List<PuestoVM> source3 = extrasDataAccess.ObtenerPuestos().ToList();
			List<UnidadVM> source4 = extrasDataAccess.ObtenerUnidades().ToList();
			List<CargaEntidadVM> list2 = new List<CargaEntidadVM>();
			for (int i = 2; i <= end.Row; i++)
			{
				string nombre_region = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string nombre_sede = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string nombre_unidad = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				string nombre_puesto = ((ExcelRangeBase)val2.Cells[i, 4]).Text;
				string nombre_genero = ((ExcelRangeBase)val2.Cells[i, 5]).Text;
				string text2 = ((ExcelRangeBase)val2.Cells[i, 6]).Text;
				CargaEntidadVM cargaEntidadVM = list2.Where((CargaEntidadVM x) => x.Region == nombre_region && x.Sede == nombre_sede && x.Unidad == nombre_unidad && x.Puesto == nombre_puesto && x.Genero == nombre_genero).FirstOrDefault();
				if (cargaEntidadVM != null)
				{
					string item = "Ya existe la combinación: " + nombre_region + ", " + nombre_sede + ", " + nombre_unidad + ", " + nombre_puesto + ", " + nombre_genero;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
					continue;
				}
				CargaEntidadVM cargaEntidadVM2 = new CargaEntidadVM();
				cargaEntidadVM2.Region = nombre_region;
				cargaEntidadVM2.Sede = nombre_sede;
				cargaEntidadVM2.Unidad = nombre_unidad;
				cargaEntidadVM2.Puesto = nombre_puesto;
				cargaEntidadVM2.Genero = nombre_genero;
				list2.Add(cargaEntidadVM2);
				GrupoVM grupoVM = grupoDataAccess.ObtenerGrupoPorNombre(text2);
				if (grupoVM == null)
				{
					grupoVM = new GrupoVM();
					grupoVM.Nombre = text2;
					grupoVM.Genero = ((nombre_genero == "F") ? "Femenino" : "Masculino");
					grupoVM.Estado = true;
					grupoVM.UsuarioCrea = "admin";
					int idGrupo = grupoDataAccess.AgregarGrupo(grupoVM);
					grupoVM.IdGrupo = idGrupo;
				}
				RegionVM regionVM = source.Where((RegionVM x) => x.Nombre == nombre_region).FirstOrDefault();
				if (regionVM == null)
				{
					string item2 = "No existe la región con nombre: " + nombre_region;
					if (!list.Contains(item2))
					{
						list.Add(item2);
					}
					continue;
				}
				SedeVM sedeVM = source2.Where((SedeVM x) => x.DetalleSede == nombre_sede).FirstOrDefault();
				if (sedeVM == null)
				{
					string item3 = "No existe la sede con nombre: " + nombre_sede;
					if (!list.Contains(item3))
					{
						list.Add(item3);
					}
					continue;
				}
				UnidadVM unidadVM = source4.Where((UnidadVM x) => x.DetalleUnidad == nombre_unidad).FirstOrDefault();
				if (unidadVM == null)
				{
					string item4 = "No existe la unidad con nombre: " + nombre_unidad;
					if (!list.Contains(item4))
					{
						list.Add(item4);
					}
					continue;
				}
				PuestoVM puestoVM = source3.Where((PuestoVM x) => x.DetallePuesto == nombre_puesto).FirstOrDefault();
				if (puestoVM == null)
				{
					string item5 = "No existe el puesto con nombre: " + nombre_puesto;
					if (!list.Contains(item5))
					{
						list.Add(item5);
					}
				}
				else
				{
					GrupoEntidadVM grupoEntidadVM = new GrupoEntidadVM();
					grupoEntidadVM.IdGrupo = grupoVM.IdGrupo;
					grupoEntidadVM.CodigoRegion = regionVM.Codigo;
					grupoEntidadVM.CodigoSede = sedeVM.CodigoSede;
					grupoEntidadVM.CodigoUnidad = unidadVM.CodigoUnidad;
					grupoEntidadVM.CodigoPuesto = puestoVM.CodigoPuesto;
					grupoDataAccess.AgregarGrupoEntidad(grupoEntidadVM);
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		foreach (string item6 in list)
		{
			if (item6 != "")
			{
				text += ", ";
			}
			text += item6;
		}
		return text;
	}

	public string CargarDataInicialGruposxUniformes()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		string text = "";
		Stream stream = new FileStream("CargaMasiva/UniformesXGrupo-simple.xlsx", FileMode.Open, FileAccess.Read);
		List<string> list = new List<string>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(stream);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress start = val2.Dimension.Start;
			ExcelCellAddress end = val2.Dimension.End;
			for (int i = 1; i <= end.Row; i++)
			{
				string text2 = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string text3 = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string text4 = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				GrupoVM grupoVM = grupoDataAccess.ObtenerGrupoPorNombre(text2);
				UniformeVM uniforme = uniformeDataAccess.ObtenerUniformePorNombre(text3);
				if (grupoVM == null || uniforme == null)
				{
					continue;
				}
				List<UniformeVM> source = uniformeDataAccess.ObtenerUniformesPorGrupo(grupoVM.IdGrupo);
				UniformeVM uniformeVM = source.FirstOrDefault((UniformeVM u) => u.IdUniforme == uniforme.IdUniforme);
				int result = 0;
				if (int.TryParse(text4, out result))
				{
					if (uniformeVM == null)
					{
						uniformeDataAccess.AgregarUniformesPorGrupo(uniforme.IdUniforme, grupoVM.IdGrupo, result);
					}
				}
				else
				{
					text = text + "Error al convertir el numero " + i + ",";
				}
			}
			return text;
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
	}

	private bool ComprobarSede(List<SedeVM> sedes, string sede)
	{
		foreach (SedeVM sede2 in sedes)
		{
			if (sede2.DetalleSede.Equals(sede))
			{
				return true;
			}
		}
		return false;
	}

	private bool ComprobarUnidad(List<UnidadVM> unidades, string unidad)
	{
		foreach (UnidadVM unidade in unidades)
		{
			if (unidade.DetalleUnidad.Equals(unidad))
			{
				return true;
			}
		}
		return false;
	}

	private bool ComprobarPuesto(List<PuestoVM> puestos, string puesto)
	{
		foreach (PuestoVM puesto2 in puestos)
		{
			if (puesto2.DetallePuesto.Equals(puesto))
			{
				return true;
			}
		}
		return false;
	}

	private bool ComprobarGenero(string genero)
	{
		if (genero.Equals("Femenino") || genero.Equals("Masculino"))
		{
			return true;
		}
		return false;
	}
}
