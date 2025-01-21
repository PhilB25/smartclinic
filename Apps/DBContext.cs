using System.Data;
using Npgsql;

namespace smartclinic.Services
{
    public class DBContext
    {
        private  readonly string _connectionString;
        public NpgsqlConnection DB;

        public  DBContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            DB = new NpgsqlConnection(_connectionString);
        }

    }
}