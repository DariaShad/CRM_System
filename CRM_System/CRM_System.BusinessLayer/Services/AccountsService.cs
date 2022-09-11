using CRM_System.API.Producer;
using CRM_System.DataLayer;
using IncredibleBackendContracts.Events;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    private readonly ILogger<AccountsService> _logger;

    private readonly IRabbitMQProducer _rabbitMq;
    public AccountsService(IAccountsRepository accountRepository, ILogger<AccountsService> logger, IRabbitMQProducer rabbitMq)
    {
        _accountRepository = accountRepository;
        _logger = logger;
        _rabbitMq = rabbitMq;
    }

    public async Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for adding account {accountDTO.LeadId}, {accountDTO.Currency}, {accountDTO.Status}");
        AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);
        await _rabbitMq.SendRatesMessage(new AccountCreatedEvent() { Id = accountDTO.Id, Currency = (IncredibleBackendContracts.Enums.Currency)accountDTO.Currency, Status = (IncredibleBackendContracts.Enums.AccountStatus)accountDTO.Status, LeadId = accountDTO.LeadId });
        return await _accountRepository.AddAccount(accountDTO);
    }

    public async Task DeleteAccount(int id, ClaimModel claim)
    {
        var account = await _accountRepository.GetAccountById(id);
        _logger.LogInformation($"Business layer: Database query for deleting account: {id} {account.LeadId}, {account.Currency}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.DeleteAccount(id);
    }

    public async Task <AccountDto> GetAccountById(int id, ClaimModel claim)
    {   
        //var leadId = _leadsRepository
        var account = await _accountRepository.GetAccountById(id);
        _logger.LogInformation($"Business layer: Database query for getting account: {id} {account.LeadId}, {account.Currency}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(id, claim); //Написано неправильно!!! нужно переделать под айди ЛИДА. А СЕЙЧАС ПОД АЙДИ АККАУНТА
        return account;
    }

    public async Task<List<AccountDto>> GetAllAccounts()
    {
        _logger.LogInformation("Business layer: Database query for getting all accounts");
        return await _accountRepository.GetAllAccounts();
    }
    public async Task <List<AccountDto>> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for getting accounts by lead id : {leadId}");
        AccessService.CheckAccessForLeadAndManager(leadId, claim);
        return await _accountRepository.GetAllAccounts();
    }

    public async Task UpdateAccount(AccountDto account, int id, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for updating account by id {id}, {account.LeadId}, {account.Status}, {account.IsDeleted}");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _accountRepository.UpdateAccount(account, id);
    }

}
