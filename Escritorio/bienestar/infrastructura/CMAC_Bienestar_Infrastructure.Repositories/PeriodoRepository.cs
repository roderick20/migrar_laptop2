using System.Collections.Generic;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class PeriodoRepository : IPeriodoRepository
{
	private readonly PeriodoDataAccess periodoDataAccess;

	public PeriodoRepository(IConfiguration configuration)
	{
		periodoDataAccess = new PeriodoDataAccess(configuration);
	}

	public int ActualizarPeriodo(PeriodoVM periodo)
	{
		PeriodoVM periodoVM = ObtenerPeriodoPorId(periodo.IdPeriodo);
		if (periodoVM.EstadoPeriodo == EstadoPeriodoEnum.Cerrado)
		{
			periodo.EstadoPeriodo = EstadoPeriodoEnum.Reabierto;
		}
		else
		{
			periodo.EstadoPeriodo = periodoVM.EstadoPeriodo;
		}
		return periodoDataAccess.ActualizarPeriodo(periodo);
	}

	public int AgregarPeriodo(PeriodoVM periodo)
	{
		periodo.EstadoPeriodo = EstadoPeriodoEnum.Abierto;
		return periodoDataAccess.AgregarPeriodo(periodo);
	}

	public int EliminarPeriodo(int idPeriodo)
	{
		return periodoDataAccess.EliminarPeriodo(idPeriodo);
	}

	public int ActualizarEstadoPeriodo(PeriodoVM periodo)
	{
		return periodoDataAccess.ActualizarEstadoPeriodo(periodo);
	}

	public PeriodoVM ObtenerPeriodoPorId(int idPeriodo)
	{
		return periodoDataAccess.ObtenerPeriodoPorId(idPeriodo);
	}

	public ICollection<PeriodoVM> ObtenerPeriodos()
	{
		return periodoDataAccess.ObtenerPeriodos();
	}

	public PeriodoVM ObtenerPeriodoActivo(string usuario, int tipoPedido)
	{
		return periodoDataAccess.ObtenerPeriodoActivo(usuario, tipoPedido);
	}
}
