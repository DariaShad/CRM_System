using CRM.DataLayer.Models;

namespace CRM_System.BusinessLayer;

public interface ILeadsService
{
    Task<int> Add(LeadDto lead);
    Task<LeadDto> GetById(int id);
    Task<LeadDto?> GetByEmail(string email);
    Task<List<LeadDto>> GetAll();
    Task Update(LeadDto lead);
    Task DeleteOrRestore(int id, bool isDeleting);
}
