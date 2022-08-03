using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using Dapper;

namespace CRM.DataLayer;

public class LeadRepository : BaseRepository, ILeadRepository
{
    //private readonly DapperContext _context;
    //public LeadRepository(DapperContext context)
    //{
    //    _context = context;
    //}

    public async Task<int> Add(LeadDto leadDto)
    {
        var id = await ConnectionString.QuerySingleAsync<int>(
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
                leadDto.Password,
                leadDto.RegistrationDate
            },
            commandType: System.Data.CommandType.StoredProcedure);

        return id;
    }

    public async Task<List<LeadDto>> GetAll()
    {
        var leads = ConnectionString.Query<LeadDto>(
            StoredProcedures.Lead_GetAll,
            commandType: System.Data.CommandType.StoredProcedure)
            .ToList();

        return leads;
    }

    public async Task<LeadDto> GetById(int id)
    {
        var lead = await ConnectionString.QueryFirstOrDefaultAsync<LeadDto>(
            StoredProcedures.Lead_GetAllInfoByLeadId,
            param: new { id },
            commandType: System.Data.CommandType.StoredProcedure);

        return lead;
    }

    public async Task Update(LeadDto leadDto)
    {
        await ConnectionString.QueryFirstOrDefaultAsync<LeadDto>(
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
                leadDto.Address
            },
            commandType: System.Data.CommandType.StoredProcedure);
    }

    public async Task DeleteOrRestore(int id, bool isDeleting)
    {
        if (isDeleting)
            await ConnectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Delete,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);
        else
            await ConnectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Restore,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);
    }
}
