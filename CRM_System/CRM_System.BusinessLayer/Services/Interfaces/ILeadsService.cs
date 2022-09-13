using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public interface ILeadsService
{
    Task<int> Add(LeadDto lead);
    Task<LeadDto> GetById(int id, ClaimModel claims);
    Task<LeadDto?> GetByEmail(string email);
    Task<List<LeadDto>> GetAll();
    Task Update(LeadDto lead, int id, ClaimModel claims);
    Task Restore(int id, bool isDeleted, ClaimModel claims);
    Task Delete(int id, bool isDeleted, ClaimModel claims);
}
