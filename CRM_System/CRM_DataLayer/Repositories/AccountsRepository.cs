using Dapper;
using System.Data;

namespace CRM_System.DataLayer;

public class AccountsRepository : BaseRepository, IAccountsRepository
{
    public AccountsRepository(IDbConnection dbConnection) : base(dbConnection)
    {
    }

    public async Task <int> AddAccount(AccountDto accountDTO)
    {
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
        var accounts = _connectionString.Query<AccountDto>(
            StoredProcedures.Account_GetAll,
            commandType: CommandType.StoredProcedure)
            .ToList();
        return accounts;
    }

    public async Task<AccountDto> GetAccountById (int id)
    {
        var account = _connectionString.QueryFirstOrDefault<AccountDto>(
           StoredProcedures.Account_GetById,
            param: new { id },
            commandType: CommandType.StoredProcedure);

        return account;
    }

    public async Task UpdateAccount(AccountDto account, int id)
    {
        _connectionString.Execute(
            StoredProcedures.Account_Update,
            param: new
            {
                account.Currency,
                account.IsDeleted
            },
             commandType: CommandType.StoredProcedure);
    }

    public async Task DeleteAccount(int accountId)
    {
        _connectionString.Execute(
            StoredProcedures.Account_Delete,
            param: new { id= accountId},
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
