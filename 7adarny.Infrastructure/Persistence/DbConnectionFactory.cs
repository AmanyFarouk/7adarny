using _7adarny.Application.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Infrastructure.Persistence
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _defaultedConnectionString;
        public DbConnectionFactory(IConfiguration configuration)
        {
            _defaultedConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection()
        => new SqlConnection(_defaultedConnectionString);
    }
}
