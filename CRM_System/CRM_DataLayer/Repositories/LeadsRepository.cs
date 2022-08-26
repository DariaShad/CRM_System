using CRM.DataLayer.Interfaces;
using CRM.DataLayer.Models;
using Dapper;
using System.Data;

namespace CRM.DataLayer;

public class LeadsRepository : BaseRepository, ILeadsRepository
{
    public LeadsRepository(IDbConnection connection) : base(connection)
    {
    }

    public async Task<int> Add(LeadDto leadDto)
    {
        var id = await _connectionString.QuerySingleAsync<int>(
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
        var leads = _connectionString.Query<LeadDto>(
            StoredProcedures.Lead_GetAll,
            commandType: System.Data.CommandType.StoredProcedure)
            .ToList();

        return leads;
    }

    public async Task<LeadDto> GetById(int id)
    {
        // SP must return accounts as well; need implement one-to-many mapping
        var lead = await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
            StoredProcedures.Lead_GetAllInfoByLeadId,
            param: new { id },
            commandType: System.Data.CommandType.StoredProcedure);

        return lead;
    }

    public async Task<LeadDto> GetByEmail(string email)
    {
        var lead = await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
            StoredProcedures.Lead_GetLeadByEmail,
            param: new { email },
            commandType: System.Data.CommandType.StoredProcedure);

        return lead;
    }

    public async Task Update(LeadDto leadDto)
    {
        await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
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
        // rewrite to single SP with @IsDeleted param
        if (isDeleting)
            await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Delete,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);
        else
            await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Restore,
                param: new { id },
                commandType: System.Data.CommandType.StoredProcedure);
    }
}
