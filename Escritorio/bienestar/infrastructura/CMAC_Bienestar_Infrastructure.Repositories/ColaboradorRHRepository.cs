using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class ColaboradorRHRepository : IColaboradorRHRepository
{
	private readonly ColaboradorRHDataAccess colaboradorRHDataAccess;

	private readonly RegionDataAccess regionDataAccess;

	public ColaboradorRHRepository(IConfiguration configuration)
	{
		colaboradorRHDataAccess = new ColaboradorRHDataAccess(configuration);
		regionDataAccess = new RegionDataAccess(configuration);
	}

	public int ActualizarColaboradorRH(ColaboradorRHVM grupo)
	{
		return colaboradorRHDataAccess.ActualizarColaboradorRH(grupo);
	}

	public int AgregarColaboradorRH(ColaboradorRHVM grupo)
	{
		return colaboradorRHDataAccess.AgregarColaboradorRH(grupo);
	}

	public int EliminarColaboradorRH(int idColaboradorRH)
	{
		return colaboradorRHDataAccess.EliminarColaboradorRH(idColaboradorRH);
	}

	public ICollection<ColaboradorRHVM> ObtenerColaboradoresRH()
	{
		ICollection<ColaboradorRHVM> collection = colaboradorRHDataAccess.ObtenerColaboradoresRH();
		foreach (ColaboradorRHVM item in collection)
		{
			item.Region = regionDataAccess.ObtenerRegionPorCodigo(item.CodigoRegion);
		}
		return collection;
	}

	public ColaboradorRHVM ObtenerColaboradorRHPorId(int idColaborador)
	{
		ColaboradorRHVM colaboradorRHVM = colaboradorRHDataAccess.ObtenerColaboradorRHPorId(idColaborador);
		colaboradorRHVM.Region = regionDataAccess.ObtenerRegionPorCodigo(colaboradorRHVM.CodigoRegion);
		return colaboradorRHVM;
	}

	public ColaboradorRHVM ObtenerColaboradorRHPorUsuario(string user)
	{
		ColaboradorRHVM colaboradorRHVM = colaboradorRHDataAccess.ObtenerColaboradorRHPorUsuario(user);
		colaboradorRHVM.Region = regionDataAccess.ObtenerRegionPorCodigo(colaboradorRHVM.CodigoRegion);
		return colaboradorRHVM;
	}

	public ColaboradorRHVM ValidarColaboradorRH(string dni, string nombresApellidos, string usuario, string unidad, string sede)
	{
		ColaboradorRHVM colaboradorRHVM = colaboradorRHDataAccess.ValidarColaboradorRH(dni, nombresApellidos, usuario, unidad, sede);
		colaboradorRHVM.Region = regionDataAccess.ObtenerRegionPorCodigo(colaboradorRHVM.CodigoRegion);
		return colaboradorRHVM;
	}

	public string CargaMasivaColaboradorRH(Stream streamCargaMasiva)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		string text = "";
		string text2 = "dd/MM/yyyy";
		List<ColaboradorRHVM> list = new List<ColaboradorRHVM>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(streamCargaMasiva);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress start = val2.Dimension.Start;
			ExcelCellAddress end = val2.Dimension.End;
			for (int i = 2; i <= end.Row; i++)
			{
				bool flag = true;
				string text3 = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string text4 = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string text5 = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				string text6 = ((ExcelRangeBase)val2.Cells[i, 4]).Text;
				string text7 = ((ExcelRangeBase)val2.Cells[i, 5]).Text;
				string text8 = ((ExcelRangeBase)val2.Cells[i, 6]).Text;
				string text9 = ((ExcelRangeBase)val2.Cells[i, 7]).Text;
				string text10 = ((ExcelRangeBase)val2.Cells[i, 8]).Text;
				string text11 = ((ExcelRangeBase)val2.Cells[i, 9]).Text;
				if (text3 == "")
				{
					text = text + "El campo DNI de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text4 == "")
				{
					text = text + "El campo Nombres Apellidos de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text5 == "")
				{
					text = text + "El campo Usuario de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text6 == "")
				{
					text = text + "El campo Sexo de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text7 == "")
				{
					text = text + "El campo Fecha de Incorporacion de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text8 == "")
				{
					text = text + "El campo Region de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text9 == "")
				{
					text = text + "El campo Unidad de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text10 == "")
				{
					text = text + "El campo Sede de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (text11 == "")
				{
					text = text + "El campo Puesto de la fila número " + i + " esta en blanco,";
					flag = false;
				}
				if (!flag)
				{
					continue;
				}
				if (DNIValido(text3))
				{
					if (SexoValido(text6))
					{
						if (FechaValida(text7, text2))
						{
							if (colaboradorRHDataAccess.ValidarColaboradorRH(text3, text4, text5, text9, text10) == null)
							{
								list.Add(new ColaboradorRHVM
								{
									DNI = text3,
									NombreApellidos = text4,
									Usuario = text5,
									Sexo = FormatSexo(text6),
									FechaIncorporacion = DateTime.ParseExact(text7, text2, CultureInfo.InvariantCulture),
									CodigoRegion = text8,
									Unidad = text9,
									Sede = text10,
									Puesto = text11,
									UsuarioCrea = "admin"
								});
							}
							else
							{
								text = text + "Ya existe un personal en RyH con los datos de la fila " + i + ",";
							}
						}
						else
						{
							text = text + "El campo Fecha de Incorporacion de la fila número " + i + " no tiene el formato correcto,";
						}
					}
					else
					{
						text = text + "El campo Sexo de la fila número " + i + " no tiene el formato correcto,";
					}
				}
				else
				{
					text = text + "El campo DNI de la fila número " + i + " solo debe contener numeros,";
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (text.Equals(""))
		{
			foreach (ColaboradorRHVM item in list)
			{
				colaboradorRHDataAccess.AgregarColaboradorRH(item);
			}
		}
		return text;
	}

	private bool DNIValido(string dni)
	{
		foreach (char c in dni)
		{
			if (c < '0' || c > '9')
			{
				return false;
			}
		}
		return true;
	}

	private bool SexoValido(string sexo)
	{
		if (sexo == "M" || sexo == "F")
		{
			return true;
		}
		return false;
	}

	private string FormatSexo(string sexo)
	{
		if (sexo == "Masculino")
		{
			return "M";
		}
		return "F";
	}

	private bool FechaValida(string fecha, string formato)
	{
		if (DateTime.TryParse(fecha, out var _))
		{
			return true;
		}
		return false;
	}
}
