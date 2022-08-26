﻿using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer.Infrastucture;

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
            throw new NotUniqueEmailException($"This email is registered already");

        lead.Password = PasswordHash.HashPassword(lead.Password);
        lead.Role = Role.Regular;
        //accounts

        return await _leadRepository.Add(lead);
    }

    public async Task<LeadDto> GetById(int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);
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

    public async Task<List<LeadDto>> GetAll() => await _leadRepository.GetAll();

    public async Task Update(LeadDto newLead, int id, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null || newLead is null)
            throw new NotFoundException($"Lead with {lead.Id} was not found");
        
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

    public async Task DeleteOrRestore(int id, bool isDeleted, ClaimModel claims)
    {
        var lead = await _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead with {id} was not found");
                
        await CheckAccess(lead, claims);
        await _leadRepository.DeleteOrRestore(id, isDeleted);
    }

    private async Task<bool> CheckEmailForUniqueness(string email) => await _leadRepository.GetByEmail(email) == default;

    // move to another class; allow admin; disallow only by Id
    private async Task CheckAccess(LeadDto lead, ClaimModel claims)
    {
        if (claims is not null && claims.Id != lead.Id &&
            claims.Email != lead.Email &&
            claims.Role != lead.Role)
            throw new AccessDeniedException($"Access denied");
    }
}
