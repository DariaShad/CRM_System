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
        bool isChecked = CheckEmailForUniqueness(lead.Email);
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

    public async Task<LeadDto> GetByEmail(string email)
    {
        var lead = await _leadRepository.GetByEmail(email);

        if (lead is null)
            throw new NotFoundException($"Lead with {email} was not found");

        else
            return lead;
    }

    public async Task<List<LeadDto>> GetAll()
    {
        var leads = _leadRepository.GetAll();

        if (leads is null)
            throw new NotFoundException($"Leads were not found");

        return await leads;
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

    private bool CheckEmailForUniqueness(string email) => _leadRepository.GetByEmail(email) == null;
}
