using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    private readonly ILogger<AccountsService> _logger;

    public AccountsService(IAccountsRepository accountRepository, ILogger<AccountsService> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim)
    {
        _logger.LogInformation("Business layer: Database query for adding account");
        AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);
        return await _accountRepository.AddAccount(accountDTO);
    }

    public async Task DeleteAccount(int id, ClaimModel claim)
    {
        _logger.LogInformation("Business layer: Database query for deleting account");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.DeleteAccount(id);
    }

    public async Task <AccountDto> GetAccountById(int id, ClaimModel claim)
    {
        _logger.LogInformation("Business layer: Database query for getting account");
        AccessService.CheckAccessForLeadAndManager(id, claim); //Написано неправильно!!! нужно переделать под айди ЛИДА. А СЕЙЧАС ПОД АЙДИ АККАУНТА
        return await _accountRepository.GetAccountById(id);
    }

    public async Task <List<AccountDto>> GetAllAccounts() => await _accountRepository.GetAllAccounts();

    public async Task <List<AccountDto>> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
    {
        _logger.LogInformation("Business layer: Database query for getting accounts by lead id");
        AccessService.CheckAccessForLeadAndManager(leadId, claim);
        return await _accountRepository.GetAllAccounts();
    }

    public async Task UpdateAccount(AccountDto account, int id, ClaimModel claim)
    {
        _logger.LogInformation("Business layer: Database query for updating account");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.UpdateAccount(account, id);
    }

}
