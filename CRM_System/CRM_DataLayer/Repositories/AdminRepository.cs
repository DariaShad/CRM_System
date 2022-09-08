using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CRM_System.DataLayer;

public class AdminRepository : BaseRepository, IAdminRepository
{
    private readonly ILogger<AdminRepository> _logger;
    public AdminRepository(IDbConnection dbConnection, ILogger<AdminRepository> logger) : base(dbConnection)
    {
        _logger = logger;
    }

    public async Task<AdminDto> GetAdminByEmail(string email)
    {
        _logger.LogInformation($"Data Layer: get admin by email {email}");
        var admin = await _connectionString.QueryFirstOrDefaultAsync<AdminDto>(
            StoredProcedures.Admin_GetAdminByEmail,
            param: new { email },
            commandType: CommandType.StoredProcedure);

        return admin;
    }

    public async Task<int> AddAdmin(AdminDto admin)
    {
        _logger.LogInformation($"Data Layer: add admin {admin.Email}");
        var id = await _connectionString.QuerySingleAsync<int>(
            StoredProcedures.Admin_Add,
            param : new 
            {
                admin.Password,
                admin.Email
            },
            commandType: CommandType.StoredProcedure);

        return id;
    }
}
