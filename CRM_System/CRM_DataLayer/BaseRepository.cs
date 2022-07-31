using System.Data;
using System.Data.SqlClient;

namespace CRM.DataLayer;

public class BaseRepository
{
    public IDbConnection ConnectionString = new SqlConnection(@"Data Source=;Initial Catalog = CRM_System.DB;Persist Security Info=True;User ID;Password=;");
}
