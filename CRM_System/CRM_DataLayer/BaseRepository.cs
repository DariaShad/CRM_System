using System.Data;
using System.Data.SqlClient;

namespace CRM_System.DataLayer;

public class BaseRepository
{
    private readonly IDbConnection _connection;

    public BaseRepository(IDbConnection dbConnection)
    {
        _connection = dbConnection;
    }

    protected IDbConnection _connectionString => new SqlConnection(_connection.ConnectionString);
}
