using System.Collections.Generic;
using CMAC_Bienestar_Core.Emuns;

namespace CMAC_Bienestar_WebAPI.Helpers;

internal class Response
{
	public RespuestaEnum Status { get; set; }

	public string Message { get; set; } = string.Empty;


	public List<Error>? Errors { get; set; }
}
