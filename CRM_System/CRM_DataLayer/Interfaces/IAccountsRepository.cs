using CRM.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataLayer.Interfaces
{
    public interface IAccountsRepository
    {
        public Task <int> AddAccount(AccountDto accountDTO);
        public Task <List<AccountDto>> GetAllAccounts();
        public Task <AccountDto> GetAccountById(int id);
        public Task UpdateAccount(AccountDto account, int id);
        public Task DeleteAccount(int id);

    }
}
