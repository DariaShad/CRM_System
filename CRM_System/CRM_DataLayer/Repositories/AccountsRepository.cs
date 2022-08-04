using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using CRM.DataLayer.StoredProcedure;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Repositories
{
    public class AccountsRepository : BaseRepository, IAccountsRepository
    {
        public int AddAccount(AccountDto accountDTO)
        {
            var id = ConnectionString.QuerySingle<int>(
                AccountStoredProcedure.Account_Add,
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
                AccountStoredProcedure.Account_GetAll,
                commandType: System.Data.CommandType.StoredProcedure)
                .ToList();
            return accounts;
        }

        //GetBalance, Last transaction date
        public AccountDto GetAccountById (int id)
        {
            var account = ConnectionString.QueryFirstOrDefault<AccountDto>(
                AccountStoredProcedure.Account_GetById,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);

            return account;
        }

        public void UpdateAccount(AccountDto account)
        {
            ConnectionString.QuerySingleOrDefault(
                AccountStoredProcedure.Account_Update,
                param: new
                {
                    account.Currency
                },
                 commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteAccount(int accountId)
        {
            ConnectionString.QuerySingleOrDefault(
                AccountStoredProcedure.Account_Delete,
                param: new { id= accountId},
                commandType: System.Data.CommandType.StoredProcedure);
        }
        //GetBalance
    }
}
