using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer;

public interface ILeadsService
{
    Task<int> Add(LeadDto lead);
    Task<LeadDto> GetById(int id, ClaimModel claims);
    Task<LeadDto?> GetByEmail(string email);
    Task<List<LeadDto>> GetAll();
    Task Update(LeadDto lead, int id, ClaimModel claims);
    Task DeleteOrRestore(int id, bool isDeleting, ClaimModel claims);
}
