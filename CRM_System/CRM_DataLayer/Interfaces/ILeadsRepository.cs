namespace CRM_System.DataLayer;

public interface ILeadsRepository
{
    Task<int> Add(LeadDto leadDto);
    Task<List<LeadDto>> GetAll();
    Task<LeadDto> GetById(int id);
    Task<LeadDto> GetByEmail(string email);
    Task Update(LeadDto leadDto);
    Task UpdateRole(LeadDto leadDto, int id);
    Task DeleteOrRestore(int id, bool isDeleting);
}