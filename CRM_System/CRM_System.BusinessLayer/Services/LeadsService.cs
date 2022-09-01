using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class LeadsService : ILeadsService
{
    private readonly ILeadsRepository _leadRepository;

    private readonly IAccountsRepository _accountRepository;

    public LeadsService(ILeadsRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<int> Add(LeadDto lead)
    {
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

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);

        return lead;
    }

    public async Task<LeadDto?> GetByEmail(string email)
    {
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
            throw new NotFoundException($"Lead with email '{email}' was not found");

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll() => await _leadRepository.GetAll();

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
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
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with id '{id}' was not found");
                
        AccessService.CheckAccessForLeadAndManager(lead.Id, claims);
        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == default;

}
