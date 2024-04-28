using System;
using System.Security.Cryptography;
using System.Text;

namespace CMAC_Bienestar_Core.Common;

public static class Util
{
	public static string EncodePassword(string password)
	{
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] bytes = Encoding.Default.GetBytes(password);
		byte[] array = ((HashAlgorithm)(object)mD5CryptoServiceProvider).ComputeHash(bytes);
		return BitConverter.ToString(array);
	}

	public static string GetExcelColumnName(int columnNumber)
	{
		string text = "";
		while (columnNumber > 0)
		{
			int num = (columnNumber - 1) % 26;
			text = Convert.ToChar(65 + num) + text;
			columnNumber = (columnNumber - num) / 26;
		}
		return text;
	}
}
