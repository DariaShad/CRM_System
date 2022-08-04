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
        public int AddAccount(AccountDto accountDTO);
        public List<AccountDto> GetAllAccounts();
        public AccountDto GetAccountById(int id);
        public void UpdateAccount(AccountDto account);
        public void DeleteAccount(int id);

    }
}
