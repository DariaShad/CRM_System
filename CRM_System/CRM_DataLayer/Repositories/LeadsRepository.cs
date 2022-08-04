using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using Dapper;

namespace CRM.DataLayer;

public class LeadRepository : BaseRepository, ILeadsRepository
{
    //private readonly DapperContext _context;
    //public LeadRepository(DapperContext context)
    //{
    //    _context = context;
    //}

    public int Add(LeadDto leadDto)
    {
        var id = ConnectionString.QuerySingle<int>(
            StoredProcedures.Lead_Add,
            param: new
            {
                leadDto.FirstName,
                leadDto.LastName,
                leadDto.Patronymic,
                leadDto.Birthday,
                leadDto.Email,
                leadDto.Phone,
                leadDto.Passport,
                leadDto.City,
                leadDto.Address,
                leadDto.Role,
                leadDto.RegistrationDate
            },
            commandType: System.Data.CommandType.StoredProcedure);

        return id;
    }

    public List<LeadDto> GetAll()
    {
        var leads = ConnectionString.Query<LeadDto>(
            StoredProcedures.Lead_GetAll,
            commandType: System.Data.CommandType.StoredProcedure)
            .ToList();

        return leads;
    }

    public LeadDto GetById(int id)
    {
        var lead = ConnectionString.QueryFirstOrDefault<LeadDto>(
            StoredProcedures.Lead_GetById,
            param: new { id },
            commandType: System.Data.CommandType.StoredProcedure);

        //var accounts = ConnectionString.Query<AccountDto>(
        //    StoredProcedures.Account_GetAllAccountsByLeadId,
        //    param: new { lead.Id },
        //    commandType: System.Data.CommandType.StoredProcedure)
        //    .ToList();

        return lead;
    }

    public void Update(LeadDto leadDto)
    {
        ConnectionString.QueryFirstOrDefault<LeadDto>(
            StoredProcedures.Account_Update,
            param: new
            {
                leadDto.FirstName,
                leadDto.LastName,
                leadDto.Patronymic,
                leadDto.Birthday,
                leadDto.Phone,
                leadDto.Passport,
                leadDto.City,
                leadDto.Address,
                leadDto.Role
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public void Delete(int id)
    {
        ConnectionString.QueryFirstOrDefault<LeadDto>(
            StoredProcedures.Lead_Delete,
            param: new { id },
            commandType: System.Data.CommandType.StoredProcedure);
    }
}
