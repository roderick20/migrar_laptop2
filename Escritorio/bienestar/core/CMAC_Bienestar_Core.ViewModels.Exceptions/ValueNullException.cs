using System;

namespace CMAC_Bienestar_Core.ViewModels.Exceptions;

public class ValueNullException : Exception
{
	public ValueNullException(string parametro)
		: base("El valor no puede ser null. (Parametro '" + parametro + "')")
	{
	}
}
