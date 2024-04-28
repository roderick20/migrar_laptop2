namespace CMAC_Bienestar_WebAPI.DTOs;

public class ItemPedidoDTOCreate
{
	public int IdUniforme { get; set; }

	public int IdTalla { get; set; }

	public int Cantidad { get; set; }

	public int IdPeriodo { get; set; }

	public int EstadoItem { get; set; } = 1;


	public int? TipoItem { get; set; }

	public string UsuarioCrea { get; set; } = string.Empty;

}
