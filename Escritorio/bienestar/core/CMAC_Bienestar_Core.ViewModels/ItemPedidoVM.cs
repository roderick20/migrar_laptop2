using CMAC_Bienestar_Core.ViewModels.Base;

namespace CMAC_Bienestar_Core.ViewModels;

public class ItemPedidoVM : AuditoriaVM
{
	public int IdItemPedido { get; set; }

	public int IdUniforme { get; set; }

	public UniformeVM? Uniforme { get; set; } = new UniformeVM();


	public int IdTalla { get; set; }

	public TallaVM? Talla { get; set; } = new TallaVM();


	public int Cantidad { get; set; }

	public int IdPeriodo { get; set; }

	public PeriodoVM Periodo { get; set; } = new PeriodoVM();


	public int EstadoItem { get; set; } = 1;


	public int? TipoItem { get; set; }
}
