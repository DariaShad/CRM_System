using System.Data;
using System.Data.SqlClient;

namespace CRM.DataLayer.Repositories
{
    public class BaseRepository
    {
        public IDbConnection ConnectionString = new SqlConnection(@"Server=DESKTOP-PMA057A; Database = CRM_System.DB; Trusted_Connection=True");
    }
}
