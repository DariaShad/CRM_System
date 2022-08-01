using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer;

public class LeadService : ILeadService
{
    private readonly ILeadRepository _leadRepository;

    public LeadService(ILeadRepository leadRepository)
    {
        _leadRepository = leadRepository;
    }

    //public int AddLead(LeadModel lead)
    //{
    //    //проверка email

    //    lead.Password = PasswordHash.HashPassword(lead.Password);

    //    //return _leadRepository.Add(lead);
    //}

    public LeadDto GetById(int id)
    {
        var client = _leadRepository.GetById(id);

        if (client is null)
            throw new NotFoundException($"Lead {id} was not found");
                
            //throw new AccessDeniedException($"Access denied");
               
        else
            return client;
    }

    public List<LeadDto> GetAll()
    {
        var clients = _leadRepository.GetAll();

        return clients;
    }

    public void Update(LeadDto lead, ClaimModel claims)
    {
        if (lead is null)
            throw new NotFoundException($"Lead {lead.Id} was not found");

        _leadRepository.Update(lead);
    }

    public void Delete(int id)
    {
        var lead = _leadRepository.GetById(id);

        if (lead is null)
            throw new NotFoundException($"Lead {id} was not found");

            //throw new AccessDeniedException($"Access denied");

        else
            _leadRepository.Delete(id);
    }



    //private bool CheckEmailForUniqueness(string email) => _leadRepository.GetByEmail(email) == null;
}
