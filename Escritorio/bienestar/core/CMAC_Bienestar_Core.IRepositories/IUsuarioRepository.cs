using System.Collections.Generic;
using System.IO;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IUsuarioRepository
{
	int ActualizarEstadoUsuario(int idUsuario, bool estado);

	int ActualizarUsuario(UsuarioVM usuario);

	int AgregarUsuario(UsuarioVM usuario);

	ICollection<UsuarioVM> ObtenerUsuarios();

	UsuarioVM ObtenerUsuarioPorNombreUsuario(string nombreUsuario);

	bool ValidarUsuarioPassword(string nombreUsuari, string passwordEncriptado);

	string CargaMasivaUsuarios(Stream streamCargaMasiva);
}
