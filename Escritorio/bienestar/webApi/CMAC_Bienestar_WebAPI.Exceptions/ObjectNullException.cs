using System;

namespace CMAC_Bienestar_WebAPI.Exceptions;

public class ObjectNullException : Exception
{
	public ObjectNullException(string message)
		: base(message)
	{
	}
}
