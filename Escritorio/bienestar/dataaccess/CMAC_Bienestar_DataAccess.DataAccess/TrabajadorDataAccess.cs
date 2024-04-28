using CMAC_Bienestar_DataAccess.Configuration;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace CMAC_Bienestar_DataAccess.DataAccess;

public class TrabajadorDataAccess
{
	private readonly OracleConnection connection;

	public TrabajadorDataAccess(IConfiguration configuration)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		BienestarConnection bienestarConnection = new BienestarConnection(configuration);
		connection = (OracleConnection)bienestarConnection.GetCMACOracleConnection();
	}
}
