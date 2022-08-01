using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer;

public interface ILeadService
{
    LeadDto GetById(int id, ClaimModel claims);
    List<LeadDto> GetAll();
    void Update(LeadDto lead, ClaimModel claims);
    void Delete(int id);
}
