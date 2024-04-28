using System;

namespace CMAC_Bienestar_Core.ViewModels.Exceptions;

public class ValueNull2Exception : Exception
{
	public ValueNull2Exception(string parametro)
		: base("El valor no puede ser null. (Parametro '" + parametro + "')")
	{
	}
}
