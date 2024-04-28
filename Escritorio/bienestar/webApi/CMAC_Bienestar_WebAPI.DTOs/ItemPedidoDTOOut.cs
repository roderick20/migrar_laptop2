namespace CMAC_Bienestar_WebAPI.DTOs;

public class ItemPedidoDTOOut
{
	public int IdItemPedido { get; set; }

	public UniformeDTOOut Uniforme { get; set; } = new UniformeDTOOut();


	public TallaDTO Talla { get; set; } = new TallaDTO();


	public int Cantidad { get; set; }

	public PeriodoDTOOut Periodo { get; set; } = new PeriodoDTOOut();


	public int EstadoItem { get; set; }

	public int? TipoItem { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;


	public bool Estado { get; set; }
}
