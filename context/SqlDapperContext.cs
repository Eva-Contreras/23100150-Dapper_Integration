using System.Data;
using Microsoft.Data.SqlClient;

namespace Orders.Api.context
{ 
    public class SqlDapperContext
    {
        private readonly IConfiguration _configuration;
        public SqlDapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection(string connectionName)
            => new SqlConnection(_configuration[$"ConnectionStrings:{connectionName}"]);
    }
}