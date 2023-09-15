using Npgsql;
using System.Data;

namespace SimpleServiceDeskBuilding.Context
{
    public class DapperContext
    {
        private readonly string _connection_string;
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection_string = _configuration.GetConnectionString("dev");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connection_string);

    }
}
