using CRM_System.BusinessLayer.Exceptions;
using CRM_System.DataLayer;
using IncredibleBackend.Messaging.Interfaces;
using IncredibleBackendContracts.Enums;
using IncredibleBackendContracts.Events;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class AccountsService : IAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    private readonly ILeadsRepository _leadRepository;

    private readonly ILogger<AccountsService> _logger;

    private readonly IMessageProducer _producer;
    public AccountsService(IAccountsRepository accountRepository, ILogger<AccountsService> logger, IMessageProducer producer, ILeadsRepository leadRepository)
    {
        _accountRepository = accountRepository;
        _logger = logger;
        _producer = producer;
        _leadRepository = leadRepository;
    }

    public async Task <int> AddAccount(AccountDto accountDTO, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for adding account {accountDTO.LeadId}, {accountDTO.Currency}, {accountDTO.Status}");
        AccessService.CheckAccessForLeadAndManager(accountDTO.Id, claim);

        var lead = await _leadRepository.GetById(accountDTO.LeadId);

        if (lead.Role == Role.Regular)
        {
            if (accountDTO.Currency != TradingCurrency.RUB
            && accountDTO.Currency != TradingCurrency.USD)
            {
                throw new RegularAccountRestrictionException("Regular lead cannot have any other account except RUB or USD");
            }
        }
        List <AccountDto> accountsOfLead = await _accountRepository.GetAllAccountsByLeadId(accountDTO.LeadId);
        var isRepeated = accountsOfLead.Any(a => a.Currency == accountDTO.Currency);
        if (isRepeated)
        {
            throw new RepeatCurrencyException($"Already have an account with this currency");
        }

        var accountId = await _accountRepository.AddAccount(accountDTO);
        await _producer.ProduceMessage(new AccountCreatedEvent() { Id = accountDTO.Id, Currency = accountDTO.Currency, LeadId = accountDTO.LeadId }, $"Account with id: { accountId} has been queued (add)");
        return accountId;
    }

    public async Task DeleteAccount(int id, ClaimModel claim)
    {
        var account = await _accountRepository.GetAccountById(id);
        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }
        _logger.LogInformation($"Business layer: Database query for deleting account: {id} {account.LeadId}, {account.Currency}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _producer.ProduceMessage(new AccountDeletedEvent() { Id = id}, $"Account with id: { account.Id} has been queued (delete)");
        await _accountRepository.DeleteAccount(id);
    }

    public async Task <AccountDto> GetAccountById(int id, ClaimModel claim)
    {   
        var account = await _accountRepository.GetAccountById(id);
        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }
        _logger.LogInformation($"Business layer: Database query for getting account: {id} {account.LeadId}, {account.Currency}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(claim.Id, claim); 
        return account;
    }

    public async Task <List<AccountDto>> GetAllAccountsByLeadId(int leadId, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for getting accounts by lead id : {leadId}");
        AccessService.CheckAccessForLeadAndManager(leadId, claim);
        return await _accountRepository.GetAllAccountsByLeadId(leadId);
    }

    public async Task UpdateAccount(AccountDto account, int id, ClaimModel claim)
    {
        _logger.LogInformation($"Business layer: Database query for updating account by id {id}, {account.Status}");
        AccessService.CheckAccessForLeadAndManager(id, claim);
        await _producer.ProduceMessage(new AccountUpdatedEvent() { Id = id, Status = account.Status }, $"Account with id: { account.Id} has been queued (update)");
        await _accountRepository.UpdateAccount(account, id);
    }

}
