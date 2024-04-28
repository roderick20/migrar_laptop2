using System;

namespace CMAC_Bienestar_Core.ViewModels.Filters;

public class FilterPedidoItem
{
	public DateTime? FechaInicio { get; set; }

	public DateTime? FechaFin { get; set; }

	public string NombreUsuario { get; set; } = string.Empty;

}
