using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer.Services.Interfaces
{
    public interface IAccountsService
    {
        public int AddAccount(AccountDto accountDTO);
        public List<AccountDto> GetAllAccounts();
        public AccountDto GetAccountById(int id, ClaimModel claim);
        public List<AccountDto> GetAllAccountsByLeadId(int leadId);
        public void UpdateAccount(AccountDto account, int id);
        public void DeleteAccount(int id);
    }
}
