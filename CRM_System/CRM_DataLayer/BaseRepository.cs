using System.Data;
using System.Data.SqlClient;

namespace CRM.DataLayer;

public class BaseRepository
{
    public IDbConnection _connection;

    public BaseRepository(IDbConnection dbConnection)
    {
        _connection = dbConnection;
    }

    public IDbConnection ConnectionString => new SqlConnection(_connection.ConnectionString);
}
