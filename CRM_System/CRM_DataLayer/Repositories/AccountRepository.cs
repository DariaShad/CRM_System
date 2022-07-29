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
    public class AccountRepository : ServerOptions
    {
        public int AddAccount(AccountDTO accountDTO)
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

        public AccountDTO GetAccountById (int id)
        {
            var account = ConnectionString.QuerySingle<AccountDTO>(
                AccountStoredProcedure.Account_GetById,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);

            return account;
        }
    }
}
