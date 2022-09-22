using CRM_System.API.Extensions;
using CRM_System.BusinessLayer.Exceptions;
using CRM_System.DataLayer;
using IncredibleBackend.Messaging.Interfaces;
using IncredibleBackendContracts.Enums;
using IncredibleBackendContracts.Events;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class LeadsService : ILeadsService
{
    private readonly ILeadsRepository _leadRepository;

    private readonly IAccountsRepository _accountRepository;

    private readonly ILogger<LeadsService> _logger;
    private readonly IMessageProducer _producer;

    public LeadsService(ILeadsRepository leadRepository, ILogger<LeadsService> logger, IMessageProducer producer, IAccountsRepository accountRepository)
    {
        _leadRepository = leadRepository;
        _logger = logger;
       _producer = producer;
        _accountRepository = accountRepository;
    }

    public async Task<int> Add(LeadDto lead)
    {
        _logger.LogInformation($"Business layer: Database query for adding lead {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive()}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        bool isUniqueEmail = await CheckEmailForUniqueness(lead.Email);

        if (!isUniqueEmail)
        {
            throw new NotUniqueEmailException($"This email is registered already");
        }    

        lead.Password = PasswordHash.HashPassword(lead.Password);
        lead.Role = Role.Regular;


        var leadId = await _leadRepository.Add(lead);

        AccountDto account = new AccountDto()
        {
            LeadId = leadId,
            Currency = TradingCurrency.RUB,
            Status = AccountStatus.Active
        };

        var accountId = await _accountRepository.AddAccount(account);
        _logger.LogInformation($"Business layer: Database query for adding account Id: {accountId} by LeadId {lead.Id}");

        await _producer.ProduceMessage<LeadCreatedEvent>(new LeadCreatedEvent() { Id = lead.Id, FirstName = lead.FirstName, LastName = lead.LastName, Patronymic = lead.Patronymic, 
            Birthday = lead.Birthday, Address = lead.Address, City = lead.City, Email = lead.Email, Passport = lead.Passport, Phone = lead.Phone, RegistrationDate = lead.RegistrationDate}, $"Lead with id: {lead.Id} has been queued");

        await _producer.ProduceMessage(new AccountCreatedEvent() { Id = accountId, Currency=account.Currency, LeadId= leadId, Status = account.Status }, $"Account with id: {accountId} has been queued");

        return leadId;
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
        if (lead == null || lead.IsDeleted == true)
        {
            throw new NotFoundException("Lead with this id was not found");
        }
        _logger.LogInformation($"Business layer: Database query for getting lead by id {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        return lead;
    }

    public async Task<LeadDto> GetByIdWithDeleted(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetLeadByIdWithDeleted(id);
        if (lead == null)
        {
            throw new NotFoundException("Lead with this id was not found");
        }
        _logger.LogInformation($"Business layer: Database query for getting lead by id {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        return lead;
    }

    public async Task<LeadDto> GetByEmail(string email)
    {
        _logger.LogInformation($"Business layer: Database query for getting lead by email {email}");
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
        {
            throw new NotFoundException($"Lead with email '{email}' was not found");
        }

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll()
    {
        _logger.LogInformation($"Business layer: Database query for getting all leads");
        return await _leadRepository.GetAll();

    }

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
        _logger.LogInformation($"Business layer: Database query for updating lead {id}, new data: {newLead.FirstName}, {newLead.LastName}, {newLead.Patronymic}, {newLead.Birthday}, {newLead.Phone.MaskNumber()}, " +
            $"{newLead.City}, {newLead.Address.MaskTheLastFive}");
        var lead = await _leadRepository.GetById(id);

        if (lead is null || newLead is null)
            throw new NotFoundException($"Lead with id '{lead.Id}' was not found");

        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);
        lead.Id = id;
        lead.FirstName = newLead.FirstName;
        lead.LastName = newLead.LastName;
        lead.Patronymic = newLead.Patronymic;
        lead.Birthday = newLead.Birthday;
        lead.Phone = newLead.Phone;
        lead.City = newLead.City;
        lead.Address = newLead.Address;

        
        await _leadRepository.Update(lead);
        await _producer.ProduceMessage(new LeadUpdatedEvent() { Id = id, FirstName = lead.FirstName, LastName = lead.LastName, Patronymic = lead.Patronymic, 
        Birthday = lead.Birthday, Phone = lead.Phone, City = lead.City, Address = lead.Address }, "");
    }

    public async Task UpdateRole(List <int> vipIds)
    {
        _logger.LogInformation($"Business layer: Database query for updating roles for leads");

       await _leadRepository.UpdateLeadsRoles(vipIds);
        await _producer.ProduceMessage(new LeadsRoleUpdatedEvent(vipIds), $"Lead's roles has been queued");
    }

    public async Task Restore(int id, bool isDeleted, ClaimModel claims)
    {
        var lead = await _leadRepository.GetLeadByIdWithDeleted(id);
        _logger.LogInformation($"Business layer: Database query for restoring lead {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        if (lead is null)
        {
            throw new NotFoundException($"Lead with id '{id}' was not found");
        }

        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }
    public async Task Delete(int id, bool isDeleted, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
        _logger.LogInformation($"Business layer: Database query for deleting lead {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        if (lead is null)
            throw new NotFoundException($"Lead with id '{id}' was not found");

        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        await _producer.ProduceMessage(new LeadDeletedEvent() { Id = id }, "");

        List<AccountDto> accounts = new List<AccountDto>();

        accounts = await _accountRepository.GetAllAccountsByLeadId(id);

        foreach (var account in accounts)
        {
            await _accountRepository.DeleteAccount(account.Id);
            await _producer.ProduceMessage(new AccountDeletedEvent() { Id = account.Id }, "");
        }


        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == default;

}
