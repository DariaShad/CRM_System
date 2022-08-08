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
    public class AccountsRepository : BaseRepository, IAccountsRepository
    {
        public AccountsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public int AddAccount(AccountDto accountDTO)
        {
            var id = ConnectionString.QuerySingle<int>(
                StoredProcedures.Account_Add,
                param: new
                {
                    accountDTO.LeadId,
                    accountDTO.Currency,
                    accountDTO.Status
                },
                commandType: System.Data.CommandType.StoredProcedure);
            return id;
                
        }

        public List <AccountDto> GetAllAccounts()
        {
            var accounts = ConnectionString.Query<AccountDto>(
                StoredProcedures.Account_GetAll,
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return accounts;
        }

        //GetBalance, Last transaction date
        public AccountDto GetAccountById (int id)
        {
            var account = ConnectionString.QueryFirstOrDefault<AccountDto>(
               StoredProcedures.Account_GetById,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);

            return account;
        }

        public void UpdateAccount(AccountDto account, int id)
        {
            ConnectionString.QuerySingleOrDefault(
                StoredProcedures.Account_Update,
                param: new
                {
                    account.Currency
                },
                 commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteAccount(int accountId)
        {
            ConnectionString.QuerySingleOrDefault(
                StoredProcedures.Account_Delete,
                param: new { id= accountId},
                commandType: System.Data.CommandType.StoredProcedure);
        }
        //GetBalance
    }
}
