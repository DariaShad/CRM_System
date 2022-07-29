using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace CRM.DataLayer.Repositories
{
    public class ServerOptions
    {
        public IDbConnection ConnectionString = new SqlConnection(@"Server=DESKTOP-PMA057A; Database = CRM_System.DB; Trusted_Connection=True");
    }
}
