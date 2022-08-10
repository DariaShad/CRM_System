using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer.Services.Interfaces;

namespace CRM_System.BusinessLayer.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountRepository;

        public AccountsService(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public int AddAccount(AccountDto accountDTO, ClaimModel claim)
        {
            CheckAccessForLeadAndManager(accountDTO.Id, claim);
            var result = _accountRepository.AddAccount(accountDTO);
            return result;
        }

        public void DeleteAccount(int id, ClaimModel claim)
        {
            _accountRepository.DeleteAccount(id);
            CheckAccessForLeadAndManager(id, claim);
        }

        public AccountDto GetAccountById(int id, ClaimModel claim)
        {
            CheckAccessForLeadAndManager(id, claim); //Написано неправильно!!! нужно переделать под айди ЛИДА
            var result = _accountRepository.GetAccountById(id);
            return result;
        }

        public List<AccountDto> GetAllAccounts() => _accountRepository.GetAllAccounts().ToList();

        public List<AccountDto> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
        {
            CheckAccessForLeadAndManager(leadId, claim);
            var result = _accountRepository.GetAllAccounts();
            return result;
        }

        public void UpdateAccount(AccountDto account, int id, ClaimModel claim)
        {
            CheckAccessForLeadAndManager(id, claim);
            _accountRepository.UpdateAccount(account, id);
        }

        private async Task CheckAccessForLeadAndManager(int id, ClaimModel claims)
        {
            if (claims is not null && claims.Id != id ||
                claims.Role != Role.Admin)
            {
                throw new AccessDeniedException($"Access denied");
            }    
                
        }

    }
}
