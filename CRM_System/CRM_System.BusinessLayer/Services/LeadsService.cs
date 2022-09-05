using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class LeadsService : ILeadsService
{
    private readonly ILeadsRepository _leadRepository;

    private readonly IAccountsRepository _accountRepository;

    private readonly ILogger<LeadsService> _logger;

    public LeadsService(ILeadsRepository leadRepository, ILogger<LeadsService> logger)
    {
        _leadRepository = leadRepository;
        _logger = logger;
    }

    public async Task<int> Add(LeadDto lead)
    {
        _logger.LogInformation("Business layer: Database query for adding lead");
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
        //_accountRepository.AddAccount(account);

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        _logger.LogInformation("Business layer: Database query for getting lead by id");
        var lead = await _leadRepository.GetById(id);
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        return lead;
    }

    public async Task<LeadDto?> GetByEmail(string email)
    {
        _logger.LogInformation("Business layer: Database query for getting lead by email");
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
            throw new NotFoundException($"Lead with email '{email}' was not found");

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll() => await _leadRepository.GetAll();

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
        _logger.LogInformation("Business layer: Database query for updating lead");
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

    public async Task DeleteOrRestore(int id, bool isDeleted, ClaimModel claims)
    {
        _logger.LogInformation("Business layer: Database query for deleting lead");
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with id '{id}' was not found");
                
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);
        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == default;

}
