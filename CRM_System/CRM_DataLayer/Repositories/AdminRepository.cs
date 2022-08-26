using CRM.DataLayer.Interfaces;
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
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        public AdminRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<AdminDto> GetAdminByEmail(string email)
        {
            var admin = await _connectionString.QueryFirstOrDefaultAsync<AdminDto>(
                StoredProcedures.Admin_GetAdminByEmail,
                param: new { email },
                commandType: CommandType.StoredProcedure);
            return admin;
        }

        public async Task<int> AddAdmin(AdminDto admin)
        {
            var id = await _connectionString.QuerySingleAsync<int>(
                StoredProcedures.Admin_Add,
                param : new 
                {
                    admin.Password,
                    admin.Email,
                    admin.Role
                },
                commandType: CommandType.StoredProcedure);

            return id;
        }
    }
}
