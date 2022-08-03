using CRM.DataLayer.Models;

namespace CRM.DataLayer.Interfaces
{
    public interface IAccountsRepository
    {
        public int AddAccount(AccountDto accountDTO);
        public List<AccountDto> GetAllAccounts();
        public AccountDto GetAccountById(int id);
        public List<AccountDto> GetAllAccountsByLeadId(int leadId);
        public void UpdateAccount(AccountDto account);
        public void DeleteAccount(int id);

    }
}
