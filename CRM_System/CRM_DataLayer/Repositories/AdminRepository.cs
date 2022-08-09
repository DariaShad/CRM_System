using CRM.DataLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repositories
{
    public class AdminRepository : BaseRepository
    {
        public AdminRepository(IDbConnection dbConnection) : base(dbConnection)
        {

        }

        public async Task<AdminDto> GetAdminByEmail(string email)
        {
            var admin = await ConnectionString.QueryFirstOrDefaultAsync<AdminDto>(
                StoredProcedures.Admin_GetAdminByEmail,
                param: new { email },
                commandType: System.Data.CommandType.StoredProcedure);
            return admin;
        }
    }
}
