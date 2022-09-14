using CRM_System.API.Producer;
using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;
using IncredibleBackendContracts.Events;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    private readonly ILeadsRepository _leadRepository;

    private readonly ILogger<AccountsService> _logger;

    private readonly IRabbitMQProducer _rabbitMq;
    public AccountsService(IAccountsRepository accountRepository, ILogger<AccountsService> logger, IRabbitMQProducer rabbitMq, ILeadsRepository leadRepository)
    {
        _accountRepository = accountRepository;
        _logger = logger;
        _rabbitMq = rabbitMq;
        _leadRepository = leadRepository;
    }

    public async Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for adding account {accountDTO.LeadId}, {accountDTO.Currency}, {accountDTO.Status}");
        AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);

        var lead = await _leadRepository.GetById(accountDTO.LeadId);
        if ((lead.Role == Role.Regular
            || lead.Role == Role.Vip)
            && (accountDTO.Currency == TradingCurrency.RUB
            || accountDTO.Currency == TradingCurrency.USD))
        {
            throw new Exception("Cannot have more than one account of the same TradingCurrency");
        }
        if (lead.Role == Role.Regular)
        {
            if (accountDTO.Currency != TradingCurrency.RUB
            || accountDTO.Currency != TradingCurrency.USD)
            {
                throw new Exception("Regular lead cannot have any other account except RUB or USD");
            }
        }

        List <AccountDto> accountsOfLead = await _accountRepository.GetAllAccountsByLeadId(accountDTO.LeadId);
        List<TradingCurrency> currencies = new List<TradingCurrency>() { TradingCurrency.EUR, TradingCurrency.EUR, TradingCurrency.USD, TradingCurrency.JPY,
        TradingCurrency.AMD, TradingCurrency.BGN, TradingCurrency.RSD, TradingCurrency.CNY};

        foreach (var account in accountsOfLead)
        {
          foreach (var currency in currencies)
            {
                if (account.Currency == currency)
                {
                    throw new Exception($"Already have an account with currency: {currency}");
                }
            }
        }

        await _rabbitMq.SendMessage(new AccountCreatedEvent() { Id = accountDTO.Id, Currency = accountDTO.Currency, Status = (IncredibleBackendContracts.Enums.AccountStatus)accountDTO.Status, LeadId = accountDTO.LeadId });
        return await _accountRepository.AddAccount(accountDTO);
    }

    public async Task DeleteAccount(int id, ClaimModel claim)
    {
        var account = await _accountRepository.GetAccountById(id);
        _logger.LogInformation($"Business layer: Database query for deleting account: {id} {account.LeadId}, {account.Currency}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _rabbitMq.SendMessage(new AccountDeletedEvent() { Id = id});
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
        await _rabbitMq.SendMessage(new AccountUpdatedEvent() { Id = id, Status= (IncredibleBackendContracts.Enums.AccountStatus)account.Status });
        await _accountRepository.UpdateAccount(account, id);
    }

}
