using System;

namespace CMAC_Bienestar_WebAPI.Exceptions;

public class BulkLoadException : Exception
{
	public BulkLoadException(string message)
		: base(ProcesarMensaje(message))
	{
	}

	private static string ProcesarMensaje(string message)
	{
		string text = "Error al procesar el archivo, las siguientes filas contienen valores nulos y no se procesaron:\n";
		string[] array = message.Split(",");
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			text = text + text2 + ".\n";
		}
		return text;
	}
}
