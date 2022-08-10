using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer;

public class LeadsService : ILeadsService
{
    private readonly ILeadsRepository _leadRepository;

    public LeadsService(ILeadsRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    public async Task<int> Add(LeadDto lead)
    {
        bool isUniqueEmail = await CheckEmailForUniqueness(lead.Email);
        if (!isUniqueEmail)
            throw new RegisteredEmailException($"This email is registered already");

        else
            lead.Password = PasswordHash.HashPassword(lead.Password);
            lead.Role = Role.Regular;
            //accounts

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with {id} was not found");
        else
            await CheckAccess(lead, claims);

            return lead;
    }

    public async Task<LeadDto?> GetByEmail(string email)
    {
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
            throw new NotFoundException($"Lead with {email} was not found");

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll()
    {
        var leads = await _leadRepository.GetAll();

        if (leads is null)
            throw new NotFoundException($"Leads were not found");

        return leads;
    }

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null || newLead is null)
            throw new NotFoundException($"Lead with {lead.Id} was not found");
        else
            await CheckAccess(lead, claims);

        lead.FirstName = newLead.FirstName;
        lead.LastName = newLead.LastName;
        lead.Patronymic = newLead.Patronymic;
        lead.Birthday = newLead.Birthday;
        lead.Phone = newLead.Phone;
        lead.City = newLead.City;
        lead.Address = newLead.Address;

        await _leadRepository.Update(lead);
    }

    public async Task DeleteOrRestore(int id, bool isDeleting, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with {id} was not found");
        else          
            await CheckAccess(lead, claims);
            await _leadRepository.DeleteOrRestore(id, isDeleting);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == null;

    private async Task CheckAccess(LeadDto lead, ClaimModel claims)
    {
        if (claims is not null && claims.Id != lead.Id &&
            claims.Email != lead.Email &&
            claims.Role != lead.Role)
            throw new AccessDeniedException($"Access denied");
    }
}
