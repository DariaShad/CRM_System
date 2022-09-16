using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CRM_System.DataLayer;

public class AccountsRepository : BaseRepository, IAccountsRepository
{
    private readonly ILogger<AccountsRepository> _logger;
    public AccountsRepository(IDbConnection dbConnection, ILogger<AccountsRepository> logger) : base(dbConnection)
    {
        _logger = logger;
    }

    public async Task <int> AddAccount(AccountDto accountDTO)
    {
        _logger.LogInformation($"Data Layer: Add account: {accountDTO.LeadId}, {accountDTO.Currency}, {accountDTO.Status}");
        var id = _connectionString.QuerySingle<int>(
            StoredProcedures.Account_Add,
            param: new
            {
                accountDTO.LeadId,
                accountDTO.Currency,
                accountDTO.Status
            },
            commandType: CommandType.StoredProcedure);
        return id;
    }

    public async Task<List <AccountDto>> GetAllAccounts()
    {
        _logger.LogInformation($"Data Layer: Get all accounts");
        var accounts = _connectionString.Query<AccountDto>(
            StoredProcedures.Account_GetAll,
            commandType: CommandType.StoredProcedure)
            .ToList();
        return accounts;
    }
    public async Task<List<AccountDto>> GetAllAccountsByLeadId(int leadId)
    {
        _logger.LogInformation($"Data Layer: Get all accounts by lead id: {leadId}");
        var accounts = ( await _connectionString.QueryAsync<AccountDto>(
            StoredProcedures.Account_GetAllAccountsByLeadId,
            param: new { leadId },
            commandType: CommandType.StoredProcedure)).ToList();
        return accounts;
    }

    public async Task<AccountDto> GetAccountById (int id)
    {
        var account = _connectionString.QueryFirstOrDefault<AccountDto>(
           StoredProcedures.Account_GetById,
            param: new { id },
            commandType: CommandType.StoredProcedure);

        _logger.LogInformation($"Data Layer: Get account by id: {account.LeadId}, {account.Currency}, {account.Status}");

        return account;
    }

    public async Task UpdateAccount(AccountDto account, int id)
    {
        _logger.LogInformation($"Data Layer: Get account by id {id}: {account.Status}");
        _connectionString.Execute(
            StoredProcedures.Account_Update,
            param: new
            {
                id,
                account.Status
            },
             commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAccount(int accountId)
    {
        _logger.LogInformation($"Data Layer: Delete account {accountId}");
        _connectionString.Execute(
            StoredProcedures.Account_Delete,
            param: new { id= accountId},
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
