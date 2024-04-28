using System;

namespace CMAC_Bienestar_Core.IRepositories;

public interface ILoggerRepository
{
	void LogInformation(string message);

	void LogInformation(string message, Exception ex);

	void LogAdvertencia(string message);

	void LogAdvertencia(string message, Exception ex);

	void LogError(string message);

	void LogError(string message, Exception ex);
}
