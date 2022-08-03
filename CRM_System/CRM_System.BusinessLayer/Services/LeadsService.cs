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
        bool isChecked = await CheckEmailForUniqueness(lead.Email);
        if (!isChecked)
            throw new RegisteredEmailException($"This email is registered already");

        else
            lead.Password = PasswordHash.HashPassword(lead.Password);
            lead.Role = Role.Regular;
        //accounts

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id)
    {
        var lead = _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with {id} was not found");

        else
            return await lead;
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

    public async Task Update(LeadDto lead)
    {
        if (lead is null)
            throw new NotFoundException($"Lead with {lead.Id} was not found");

        await _leadRepository.Update(lead);
    }

    public async Task DeleteOrRestore(int id, bool isDeleting)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with {id} was not found");

        else
            await _leadRepository.DeleteOrRestore(id, isDeleting);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == null;
}
