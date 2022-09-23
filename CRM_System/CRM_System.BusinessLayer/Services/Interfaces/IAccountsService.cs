using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public interface IAccountsService
{
    public Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim);
    public Task <AccountDto> GetAccountById(int id, ClaimModel claim);
    public Task <List<AccountDto>> GetAllAccountsByLeadId(int leadId, ClaimModel claim);
    public Task UpdateAccount(AccountDto account, int id, ClaimModel claim);
    public Task DeleteAccount(int id, ClaimModel claim);
}
