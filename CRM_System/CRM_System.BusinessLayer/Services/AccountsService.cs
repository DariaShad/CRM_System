using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    public AccountsService(IAccountsRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim)
    {
        AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);
        return await _accountRepository.AddAccount(accountDTO);
    }

    public async Task DeleteAccount(int id, ClaimModel claim)
    {
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.DeleteAccount(id);
    }

    public async Task <AccountDto> GetAccountById(int id, ClaimModel claim)
    {
        AccessService.CheckAccessForLeadAndManager(id, claim); //Написано неправильно!!! нужно переделать под айди ЛИДА. А СЕЙЧАС ПОД АЙДИ АККАУНТА
        return await _accountRepository.GetAccountById(id);
    }

    public async Task <List<AccountDto>> GetAllAccounts() => await _accountRepository.GetAllAccounts();

    public async Task <List<AccountDto>> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
    {
        AccessService.CheckAccessForLeadAndManager(leadId, claim);
        return await _accountRepository.GetAllAccounts();
    }

    public async Task UpdateAccount(AccountDto account, int id, ClaimModel claim)
    {
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.UpdateAccount(account, id);
    }

}
