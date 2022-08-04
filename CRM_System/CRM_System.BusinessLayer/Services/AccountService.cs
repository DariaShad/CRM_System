using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_System.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountsRepository _accountRepository;

        public AccountService(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public int AddAccount(AccountDto accountDTO)
        {
            var result = _accountRepository.AddAccount(accountDTO);
            return result;
        }

        public void DeleteAccount(int id)
        {
            _accountRepository.DeleteAccount(id);
        }

        public AccountDto GetAccountById(int id)
        {
            var result = _accountRepository.GetAccountById(id);
            return result;
        }

        public List<AccountDto> GetAllAccounts() => _accountRepository.GetAllAccounts().ToList();

        public List<AccountDto> GetAllAccountsByLeadId(int leadId)
        {
            var result = _accountRepository.GetAllAccounts();
            return result;
        }

        public void UpdateAccount(AccountDto account)
        {
            _accountRepository.UpdateAccount(account);
        }
    }
}
