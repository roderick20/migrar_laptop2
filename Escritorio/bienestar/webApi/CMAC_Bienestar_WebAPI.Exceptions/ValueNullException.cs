using System;

namespace CMAC_Bienestar_WebAPI.Exceptions;

public class ValueNullException : Exception
{
	public ValueNullException(string message)
		: base("El valor no puede ser null. (Parametro '" + message + "')")
	{
	}
}
