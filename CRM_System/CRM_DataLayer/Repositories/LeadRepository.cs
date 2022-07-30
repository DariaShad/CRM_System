using CRM.DataLayer.Models;

namespace CRM.DataLayer;

public class LeadRepository : BaseRepository
{
    public int AddLead(LeadDto leadDto)
    {
        var id = _connectionString.QuerySingle<int>(
            StoredProcedures.Lead_Add,
            param: new
            {
                LeadDto.FirstName,
                LeadDto.LastName,
                LeadDto.Patronymic,
                LeadDto.Birthday,
                LeadDto.Email,
                LeadDto.Phone,
                LeadDto.Passport,
                LeadDto.Address,
                LeadDto.Role,
                LeadDto.RegistrationDate
            },
            commandType: System.Data.CommandType.StoredProcedure);
        return id;
    }
}
