using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using Dapper;
using System.Data;

namespace CRM.DataLayer.Repositories
{
    public class AccountsRepository : BaseRepository, IAccountsRepository
    {
        public AccountsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        // rewrite all methods to async

        public int AddAccount(AccountDto accountDTO)
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

        public List <AccountDto> GetAllAccounts()
        {
            var accounts = _connectionString.Query<AccountDto>(
                StoredProcedures.Account_GetAll,
                commandType: CommandType.StoredProcedure)
                .ToList();
            return accounts;
        }

        //GetBalance, Last transaction date
        public AccountDto GetAccountById (int id)
        {
            var account = _connectionString.QueryFirstOrDefault<AccountDto>(
               StoredProcedures.Account_GetById,
                param: new { id },
                commandType: CommandType.StoredProcedure);

            return account;
        }

        public void UpdateAccount(AccountDto account, int id)
        {
            _connectionString.Execute(
                StoredProcedures.Account_Update,
                param: new
                {
                    account.Currency
                    // isDeleted
                },
                 commandType: CommandType.StoredProcedure);
        }

        public void DeleteAccount(int accountId)
        {
            _connectionString.Execute(
                StoredProcedures.Account_Delete,
                param: new { id= accountId},
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
