using Import.Domain;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Volo.Abp.DependencyInjection;

namespace Programarcr.Contabilidad.DbMigratorTests
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IConnectionProvider))]
    public class TestConnectionProvider: IConnectionProvider
    {
        public string ConnectionString { get; set; }
        public TestConnectionProvider(IConfiguration configuration)
        {
            ConnectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=Integrity;Trusted_Connection=True;MultipleActiveResultSets=true";
        }


        public IDbConnection GetConnection()
        {
            var conn =  new SqlConnection(ConnectionString);
            conn.Open();

            return conn;
        }
    }
}
