namespace CRM_System.DataLayer;

public interface IAccountsRepository
{
    public Task <int> AddAccount(AccountDto accountDTO);
    public Task <List<AccountDto>> GetAllAccounts();
    Task<List<AccountDto>> GetAllAccountsByLeadId(int leadId);
    public Task <AccountDto> GetAccountById(int id);
    public Task UpdateAccount(AccountDto account, int id);
    public Task DeleteAccount(int id);
}
