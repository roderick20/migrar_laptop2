using System;
using System.IO;
using System.Reflection;
using System.Xml;
using CMAC_Bienestar_Core.IRepositories;
using log4net;
using log4net.Config;
using log4net.Core;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;

namespace CMAC_Bienestar_Infrastructure.Repositories;

public class LoggerRepository : ILoggerRepository
{
	private readonly ILog _logger = LogManager.GetLogger(typeof(LoggerManager));

	public LoggerRepository()
	{
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			using FileStream inStream = File.OpenRead("log4net.config");
			xmlDocument.Load(inStream);
			ILoggerRepository val = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(Hierarchy));
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			((ContextPropertiesBase)GlobalContext.Properties)["FilePath"] = directoryName.Substring(6, directoryName.Length - 6);
			XmlConfigurator.Configure(val, xmlDocument["log4net"]);
		}
		catch (Exception ex)
		{
			_logger.Error((object)"Error", ex);
		}
	}

	public void LogInformation(string message)
	{
		_logger.Info((object)message);
	}

	public void LogInformation(string message, Exception ex)
	{
		_logger.Info((object)message, ex);
	}

	public void LogAdvertencia(string message)
	{
		_logger.Warn((object)message);
	}

	public void LogAdvertencia(string message, Exception ex)
	{
		_logger.Warn((object)message, ex);
	}

	public void LogError(string message)
	{
		_logger.Error((object)message);
	}

	public void LogError(string message, Exception ex)
	{
		_logger.Error((object)message, ex);
	}
}
