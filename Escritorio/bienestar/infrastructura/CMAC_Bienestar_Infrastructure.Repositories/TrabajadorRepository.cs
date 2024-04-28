using System.Collections.Generic;
using System.Linq;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class TrabajadorRepository : ITrabajadorRepository
{
	private readonly ExtrasDataAccess extrasDataAccess;

	private readonly ColaboradorRHDataAccess colaboradorRHDataAccess;

	public TrabajadorRepository(IConfiguration configuration)
	{
		extrasDataAccess = new ExtrasDataAccess(configuration);
		colaboradorRHDataAccess = new ColaboradorRHDataAccess(configuration);
	}

	public ICollection<TrabajadorVM> ObtenerTrabajadores()
	{
		ICollection<TrabajadorVM> collection = extrasDataAccess.ObtenerTrabajadores();
		ICollection<ColaboradorRHVM> collection2 = colaboradorRHDataAccess.ObtenerColaboradoresRH();
		foreach (ColaboradorRHVM item in collection2)
		{
			TrabajadorVM trabajadorVM = new TrabajadorVM();
			trabajadorVM.IdColaborador = item.IdColaborador;
			trabajadorVM.Nombre = item.NombreApellidos;
			trabajadorVM.NombreUsuario = item.Usuario;
			trabajadorVM.Dni = item.DNI;
			trabajadorVM.CodigoSede = item.Sede;
			trabajadorVM.CodigoUnidad = item.Unidad;
			trabajadorVM.CodigoPuesto = item.Puesto;
			trabajadorVM.FechaIngresoCorporacion = item.FechaIncorporacion;
			collection.Add(trabajadorVM);
		}
		return collection.OrderBy((TrabajadorVM x) => x.Nombre).ToList();
	}

	public ICollection<TrabajadorVM> ObtenerTrabajadoresYColaboradoresSinUsuarios()
	{
		ICollection<TrabajadorVM> source = extrasDataAccess.ObtenerTrabajadoresYColaboradoresSinUsuarios();
		return source.ToList();
	}

	public TrabajadorVM ObtenerTrabajadorPorNombreUsuario(string nombreUsuario)
	{
		return extrasDataAccess.ObtenerTrabajadorPorNombreUsuario(nombreUsuario);
	}
}
