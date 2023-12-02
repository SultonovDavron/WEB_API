using Oracle.ManagedDataAccess.Client;

namespace WEB_API.Context
{
    public interface IDBContext
    {
        OracleCommand GetCommand();
        OracleConnection GetConn();
    }
}
