namespace CMAC_Bienestar_Core.ViewModels;

public class EmailVM
{
	public string destinatarios { get; set; } = string.Empty;


	public string? conCopias { get; set; }

	public string asunto { get; set; } = string.Empty;


	public string mensaje { get; set; } = string.Empty;

}
