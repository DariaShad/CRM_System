using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace CRM_System.DataLayer;

public class LeadsRepository : BaseRepository, ILeadsRepository
{
    private readonly ILogger<LeadsRepository> _logger;
    public LeadsRepository(IDbConnection connection, ILogger<LeadsRepository> logger) : base(connection)
    {
        _logger = logger;
    }

    public async Task<int> Add(LeadDto leadDto)
    {
        _logger.LogInformation($"Data Layer: Add lead: {leadDto.FirstName}, {leadDto.LastName}, {leadDto.Patronymic}, {leadDto.Birthday}, {leadDto.City}");
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
                leadDto.Password
            },
            commandType: System.Data.CommandType.StoredProcedure);

        return id;
    }

    public async Task<List<LeadDto>> GetAll()
    {
        _logger.LogInformation($"Data Layer: Get all leads");
        var leads = _connectionString.Query<LeadDto>(
            StoredProcedures.Lead_GetAll,
            commandType: System.Data.CommandType.StoredProcedure)
            .ToList();

        return leads;
    }

    //public async Task<LeadDto> GetById(int id)
    //{
    //    var lead = await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
    //        StoredProcedures.Lead_GetAllInfoByLeadId,
    //        param: new { IdLead = id },
    //        commandType: System.Data.CommandType.StoredProcedure);
    //    _logger.LogInformation($"Data Layer: Get by id {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}");

    //    return lead;
    //}

    public async Task<LeadDto> GetById(int id)
    {
        var lead = (await _connectionString.QueryAsync<LeadDto, AccountDto, LeadDto>(
            StoredProcedures.Lead_GetAllInfoByLeadId,
            (lead, account) =>
            {
                lead.Accounts.Add(account);
                return lead;
            },
            splitOn: "Id",
            param: new { Id = id },
            commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

        _logger.LogInformation($"Data Layer: Get by id {id}, {lead.FirstName}, {lead.LastName}, {lead.Patronymic}");

        return lead;
    }

    public async Task<LeadDto> GetByEmail(string email)
    {
        _logger.LogInformation($"Data Layer: Get by email {email}");
        var lead = await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
            StoredProcedures.Lead_GetLeadByEmail,
            param: new { email },
            commandType: System.Data.CommandType.StoredProcedure);

        return lead;
    }

    public async Task Update(LeadDto leadDto)
    {
        await _connectionString.QueryFirstOrDefaultAsync(
            StoredProcedures.Lead_Update,
            param: new
            {
                leadDto.Id,
                leadDto.FirstName,
                leadDto.LastName,
                leadDto.Patronymic,
                leadDto.Birthday,
                leadDto.Phone,
                leadDto.City,
                leadDto.Address
            },
            commandType: System.Data.CommandType.StoredProcedure);

    }

    public async Task DeleteOrRestore(int id, bool isDeleting)
    {
        if (isDeleting)
            await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Delete,
                param: new { id, IsDeleted=true },
                commandType: System.Data.CommandType.StoredProcedure);
        else
            await _connectionString.QueryFirstOrDefaultAsync<LeadDto>(
                StoredProcedures.Lead_Delete,
                param: new { id, IsDeleted=false },
                commandType: System.Data.CommandType.StoredProcedure);
    }


}