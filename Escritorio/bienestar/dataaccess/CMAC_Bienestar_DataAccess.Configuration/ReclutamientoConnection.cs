using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CMAC_Bienestar_DataAccess.Configuration;

public class ReclutamientoConnection
{
	private IDbConnection connection;

	private IConfiguration configuration;

	public ReclutamientoConnection(IConfiguration configuration)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		this.configuration = configuration;
		connection = (IDbConnection)new SqlConnection(this.configuration.GetConnectionString("ReclutamientoConnectionMD"));
	}

	public IDbConnection GetCMACSqlServerConnection()
	{
		return connection;
	}
}
