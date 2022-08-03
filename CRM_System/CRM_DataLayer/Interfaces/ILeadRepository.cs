using CRM.DataLayer.Models;

namespace CRM.DataLayer.Interfaces
{
    public interface ILeadRepository
    {
        Task<int> Add(LeadDto leadDto);
        Task<List<LeadDto>> GetAll();
        Task<LeadDto> GetById(int id);
        Task Update(LeadDto leadDto);
        Task DeleteOrRestore(int id, bool isDeleting);
    }
}