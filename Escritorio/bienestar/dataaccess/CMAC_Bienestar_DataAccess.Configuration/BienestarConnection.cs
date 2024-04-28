using System.Data;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace CMAC_Bienestar_DataAccess.Configuration;

public class BienestarConnection
{
	private IDbConnection connection;

	private IConfiguration configuration;

	public BienestarConnection(IConfiguration configuration)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		this.configuration = configuration;
		connection = (IDbConnection)new OracleConnection(this.configuration.GetConnectionString("BienestarConnection7364"));
	}

	public IDbConnection GetCMACOracleConnection()
	{
		return connection;
	}
}
