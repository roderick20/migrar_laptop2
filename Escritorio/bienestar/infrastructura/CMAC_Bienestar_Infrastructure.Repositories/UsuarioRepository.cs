using System;
using System.Collections.Generic;
using System.IO;
using CMAC_Bienestar_Core.Common;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
	private readonly UsuarioDataAccess usuarioDataAccess;

	private readonly RolDataAccess rolDataAccess;

	private readonly ExtrasDataAccess extrasDataAccess;

	private readonly RegionDataAccess regionDataAccess;

	private readonly ColaboradorRHDataAccess colaboradorDataAccess;

	public UsuarioRepository(IConfiguration configuration)
	{
		usuarioDataAccess = new UsuarioDataAccess(configuration);
		rolDataAccess = new RolDataAccess(configuration);
		extrasDataAccess = new ExtrasDataAccess(configuration);
		regionDataAccess = new RegionDataAccess(configuration);
		colaboradorDataAccess = new ColaboradorRHDataAccess(configuration);
	}

	public int ActualizarEstadoUsuario(int idUsuario, bool estado)
	{
		return usuarioDataAccess.ActualizarEstadoUsuario(idUsuario, estado);
	}

	public int ActualizarUsuario(UsuarioVM usuario)
	{
		return usuarioDataAccess.ActualizarUsuario(usuario);
	}

	public int AgregarUsuario(UsuarioVM usuario)
	{
		return usuarioDataAccess.AgregarUsuario(usuario);
	}

	public ICollection<UsuarioVM> ObtenerUsuarios()
	{
		return usuarioDataAccess.ObtenerUsuarios();
	}

	public UsuarioVM ObtenerUsuarioPorNombreUsuario(string nombreUsuario)
	{
		return usuarioDataAccess.ObtenerUsuarioPorNombreUsuario(nombreUsuario);
	}

	public bool ValidarUsuarioPassword(string nombreUsuario, string passwordEncriptado)
	{
		UsuarioVM usuarioVM = usuarioDataAccess.ObtenerUsuarioPorNombreUsuario(nombreUsuario);
		return string.Compare(passwordEncriptado, usuarioVM.Password, StringComparison.InvariantCultureIgnoreCase) == 0;
	}

	public string CargaMasivaUsuarios(Stream streamCargaMasiva)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		string text = "";
		List<UsuarioVM> list = new List<UsuarioVM>();
		ExcelPackage.LicenseContext = (LicenseContext)0;
		ExcelPackage val = new ExcelPackage();
		try
		{
			val.Load(streamCargaMasiva);
			ExcelWorksheet val2 = val.Workbook.Worksheets[0];
			ExcelCellAddress end = val2.Dimension.End;
			for (int i = 2; i <= end.Row; i++)
			{
				bool flag = true;
				string text2 = ((ExcelRangeBase)val2.Cells[i, 1]).Text;
				string text3 = ((ExcelRangeBase)val2.Cells[i, 2]).Text;
				string text4 = ((ExcelRangeBase)val2.Cells[i, 3]).Text;
				string text5 = ((ExcelRangeBase)val2.Cells[i, 4]).Text;
				bool flag2 = true;
				if (text2 == "")
				{
					text = text + "El campo USUARIO de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text3 == "")
				{
					text = text + "El campo CORREO de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text5 == "")
				{
					text = text + "El campo ROL de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				if (text4 == "")
				{
					text = text + "El campo ACTIVE DIRECTORY  de la fila número \"" + i + "\" esta en blanco,";
					flag = false;
				}
				else if (text4.Equals("NO"))
				{
					flag2 = false;
				}
				if (!flag)
				{
					continue;
				}
				if (usuarioDataAccess.ObtenerUsuarioPorNombreUsuario(text2) == null)
				{
					RolVM rolVM = rolDataAccess.ObtenerRolPorNombre(text5);
					if (flag2)
					{
						TrabajadorVM trabajadorVM = extrasDataAccess.ObtenerTrabajadorPorNombreUsuario(text2);
						if (trabajadorVM != null)
						{
							if (rolVM != null)
							{
								list.Add(new UsuarioVM
								{
									Nombre = trabajadorVM.Nombre,
									NombreUsuario = trabajadorVM.NombreUsuario,
									Password = "",
									ActiveDirectory = true,
									Dni = trabajadorVM.Dni,
									Email = text3,
									employee_id = trabajadorVM.employee_id,
									IdRol = rolVM.IdRol,
									UsuarioCrea = "admin"
								});
							}
							else
							{
								text = text + "No existe un ROL en el registro de la fila " + i + ",";
							}
						}
						else
						{
							text = text + "No existe un trabajador con el usuario de la fila " + i + ",";
						}
						continue;
					}
					ColaboradorRHVM colaboradorRHVM = colaboradorDataAccess.ObtenerColaboradorRHPorUsuario(text2);
					if (colaboradorRHVM != null)
					{
						if (rolVM != null)
						{
							string password = Util.EncodePassword("12345678");
							list.Add(new UsuarioVM
							{
								Nombre = colaboradorRHVM.NombreApellidos,
								NombreUsuario = colaboradorRHVM.Usuario,
								Password = password,
								ActiveDirectory = false,
								Dni = colaboradorRHVM.DNI,
								Email = text3,
								employee_id = "",
								IdRol = rolVM.IdRol,
								UsuarioCrea = "admin"
							});
						}
						else
						{
							text = text + "No existe un ROL en el registro de la fila " + i + ",";
						}
					}
					else
					{
						text = text + "No existe un Colaborador con el usuario de la fila " + i + ",";
					}
				}
				else
				{
					text = text + "Ya existe un Usuario con los datos de la fila " + i + ",";
				}
			}
		}
		finally
		{
			((IDisposable)val)?.Dispose();
		}
		if (text.Equals(""))
		{
			foreach (UsuarioVM item in list)
			{
				usuarioDataAccess.AgregarUsuario(item);
			}
		}
		return text;
	}
}
