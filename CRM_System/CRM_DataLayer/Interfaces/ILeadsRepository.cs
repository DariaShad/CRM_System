namespace CRM_System.DataLayer;

public interface ILeadsRepository
{
    Task<int> Add(LeadDto leadDto);
    Task<List<LeadDto>> GetAll();
    Task<LeadDto> GetById(int id);
    Task<LeadDto> GetByEmail(string email);
    Task Update(LeadDto leadDto);
    Task UpdateLeadsRoles(List<int> vipIds);
    Task DeleteOrRestore(int id, bool isDeleting);
}