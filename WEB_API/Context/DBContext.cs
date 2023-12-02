using Oracle.ManagedDataAccess.Client;

namespace WEB_API.Context
{
    public class DBContext : IDBContext
    {
        private IConfiguration _config;
        private OracleConnection _connection;
        private OracleCommand _cmd;
        private string _connectionString;

        public DBContext(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }
        public OracleConnection GetConn()
        {
            _connection = new OracleConnection(_connectionString);
            return _connection;
        }
        public OracleCommand GetCommand()
        {
            _cmd = _connection.CreateCommand();
            return _cmd;
        }
    }
}
