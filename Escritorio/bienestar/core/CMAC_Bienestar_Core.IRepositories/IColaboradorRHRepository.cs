using System.Collections.Generic;
using System.IO;
using CMAC_Bienestar_Core.ViewModels;

namespace CMAC_Bienestar_Core.IRepositories;

public interface IColaboradorRHRepository
{
	int ActualizarColaboradorRH(ColaboradorRHVM colaborador);

	int AgregarColaboradorRH(ColaboradorRHVM colaborador);

	int EliminarColaboradorRH(int idColaborador);

	ICollection<ColaboradorRHVM> ObtenerColaboradoresRH();

	ColaboradorRHVM ObtenerColaboradorRHPorId(int idColaborador);

	ColaboradorRHVM ObtenerColaboradorRHPorUsuario(string user);

	ColaboradorRHVM ValidarColaboradorRH(string dni, string nombresApellidos, string usuario, string unidad, string sede);

	string CargaMasivaColaboradorRH(Stream streamCargaMasiva);
}
