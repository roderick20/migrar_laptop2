using System.Collections.Generic;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface ITrabajadorRepository
{
	ICollection<TrabajadorVM> ObtenerTrabajadores();

	TrabajadorVM ObtenerTrabajadorPorNombreUsuario(string nombreUsuario);

	ICollection<TrabajadorVM> ObtenerTrabajadoresYColaboradoresSinUsuarios();
}
