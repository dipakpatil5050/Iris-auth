using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace IrisAuth.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly string _connectionString;

        protected RepositoryBase()
        {
            _connectionString = ConfigurationManager
                .ConnectionStrings["MVVMLoginDb"]
                .ConnectionString;
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
