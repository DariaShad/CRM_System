using CRM.DataLayer.Models;

namespace CRM.DataLayer.Interfaces
{
    public interface ILeadsRepository
    {
        int Add(LeadDto leadDto);
        void Delete(int id);
        List<LeadDto> GetAll();
        LeadDto GetById(int id);
        void Update(LeadDto leadDto);
    }
}