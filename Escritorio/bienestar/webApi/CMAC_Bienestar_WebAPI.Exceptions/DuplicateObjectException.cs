using System;

namespace CMAC_Bienestar_WebAPI.Exceptions;

public class DuplicateObjectException : Exception
{
	public DuplicateObjectException(string message)
		: base(message)
	{
	}
}
