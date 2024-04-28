using System.Collections.Generic;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class RolRepository : IRolRepository
{
	private readonly RolDataAccess rolDataAccess;

	public RolRepository(IConfiguration configuration)
	{
		rolDataAccess = new RolDataAccess(configuration);
	}

	public int AgregarRol(RolVM rol)
	{
		return rolDataAccess.AgregarRol(rol);
	}

	public ICollection<RolVM> ObtenerRoles()
	{
		return rolDataAccess.ObtenerRoles();
	}

	public RolVM ObtenerRolPorNombre(string nombre)
	{
		return rolDataAccess.ObtenerRolPorNombre(nombre);
	}
}
