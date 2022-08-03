using System.Data;
using System.Data.SqlClient;

namespace CRM.DataLayer;

public class BaseRepository
{
    public IDbConnection ConnectionString = new SqlConnection
        (@"Data Source=80.78.240.16;Initial Catalog = CRM.Db;Persist Security Info=True;Integrated Security=true;Encrypt=false;User ID=Student;Password=qwe!23;");
}
