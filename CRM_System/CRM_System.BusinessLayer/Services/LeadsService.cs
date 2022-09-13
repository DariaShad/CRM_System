using CRM_System.API.Extensions;
using CRM_System.API.Producer;
using CRM_System.DataLayer;
using IncredibleBackendContracts.Events;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class LeadsService : ILeadsService
{
    private readonly ILeadsRepository _leadRepository;

    private readonly IAccountsRepository _accountRepository;

    private readonly ILogger<LeadsService> _logger;
    private readonly IRabbitMQProducer _rabbitMq;

    public LeadsService(ILeadsRepository leadRepository, ILogger<LeadsService> logger, IRabbitMQProducer rabbitMq)
    {
        _leadRepository = leadRepository;
        _logger = logger;
        _rabbitMq = rabbitMq;
    }

    public async Task<int> Add(LeadDto lead)
    {
        _logger.LogInformation($"Business layer: Database query for adding lead {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive()}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");
        bool isUniqueEmail = await CheckEmailForUniqueness(lead.Email);
        if (!isUniqueEmail)
            throw new NotUniqueEmailException($"This email is registered already");

        lead.Password = PasswordHash.HashPassword(lead.Password);
        lead.Role = Role.Regular;

        AccountDto account = new AccountDto()
        {
            Currency = Currency.RUB,
            Status = AccountStatus.Active,
            LeadId = lead.Id,
        };
        _accountRepository.AddAccount(account);

        await _rabbitMq.SendMessage(new LeadCreatedEvent() { Id = lead.Id });

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
        _logger.LogInformation($"Business layer: Database query for getting lead by id {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        return lead;
    }
    //Update role
    public async Task<LeadDto?> GetByEmail(string email)
    {
        _logger.LogInformation($"Business layer: Database query for getting lead by email {email}");
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
            throw new NotFoundException($"Lead with email '{email}' was not found");

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll() => await _leadRepository.GetAll();

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
        _logger.LogInformation($"Business layer: Database query for updating lead {id}, new data: {newLead.FirstName}, {newLead.LastName}, {newLead.Patronymic}, {newLead.Birthday}, {newLead.Phone.MaskNumber()}, " +
            $"{newLead.City}, {newLead.Address.MaskTheLastFive}");
        var lead = await _leadRepository.GetById(id);

        if (lead is null || newLead is null)
            throw new NotFoundException($"Lead with id '{lead.Id}' was not found");

        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        lead.FirstName = newLead.FirstName;
        lead.LastName = newLead.LastName;
        lead.Patronymic = newLead.Patronymic;
        lead.Birthday = newLead.Birthday;
        lead.Phone = newLead.Phone;
        lead.City = newLead.City;
        lead.Address = newLead.Address;
        
        await _leadRepository.Update(lead);
    }

    public async Task Restore(int id, bool isDeleted, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
        _logger.LogInformation($"Business layer: Database query for restoring lead {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}, {lead.Birthday}, {lead.Phone.MaskNumber()}, " +
            $"{lead.City}, {lead.Address.MaskTheLastFive}, {lead.Email.MaskEmail()}, {lead.Passport.MaskPassport()}");

        if (lead is null)
            throw new NotFoundException($"Lead with id '{id}' was not found");
                
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);
        
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

        await _rabbitMq.SendMessage(new LeadDeletedEvent() { Id = id });

        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == default;

}
