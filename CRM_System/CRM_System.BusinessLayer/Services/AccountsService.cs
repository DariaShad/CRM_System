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
            AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);
            return _accountRepository.AddAccount(accountDTO);
        }

        public void DeleteAccount(int id, ClaimModel claim)
        {
            AccessService.CheckAccessForLeadAndManager(id, claim);
            _accountRepository.DeleteAccount(id);
        }

        public AccountDto GetAccountById(int id, ClaimModel claim)
        {
            AccessService.CheckAccessForLeadAndManager(id, claim); //Написано неправильно!!! нужно переделать под айди ЛИДА
            return _accountRepository.GetAccountById(id);
        }

        public List<AccountDto> GetAllAccounts() => _accountRepository.GetAllAccounts();

        public List<AccountDto> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
        {
            AccessService.CheckAccessForLeadAndManager(leadId, claim);
            return _accountRepository.GetAllAccounts();
        }

        public void UpdateAccount(AccountDto account, int id, ClaimModel claim)
        {
            AccessService.CheckAccessForLeadAndManager(id, claim);
            _accountRepository.UpdateAccount(account, id);
        }

    }
}
