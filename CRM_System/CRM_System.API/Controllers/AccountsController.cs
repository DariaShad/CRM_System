using AutoMapper;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;
using CRM_System.BusinessLayer;
using CRM_System.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountService;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController (IAccountsService accountService, IMapper mapper, ILogger<AccountsController> logger)
    {
        _accountService = accountService;
        _mapper = mapper;
        _logger=logger;
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task <ActionResult<AccountResponse>> GetAccount(int id)
    {
        _logger.LogInformation($"Controller: Get an account by id {id}");
        var claim = this.GetClaims();
        var result = await _accountService.GetAccountById(id, claim);
        if (result == null)
            return NotFound();
        else
            return Ok(_mapper.Map<AccountResponse>(result));
    }

    [Authorize]
    [HttpGet("/leads/{leadId}/accounts")]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult<List<AccountResponse>>> GetAllAccountsByLeadId(int leadId)
    {
        _logger.LogInformation($"Controller: Get all accounts by lead id {leadId}");
        var claim = this.GetClaims();
        var result = await _accountService.GetAllAccountsByLeadId(leadId, claim);
        return Ok(_mapper.Map<List<AccountResponse>>(result));
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task <ActionResult<int>> AddAccount([FromBody] AddAccountRequest accountRequest)
    {
        _logger.LogInformation($"Controller: Add an account: LeadId:{accountRequest.LeadId}, Currency:{accountRequest.Currency}");
        var claim = this.GetClaims();
        var result= await _accountService.AddAccount(_mapper.Map<AccountDto>(accountRequest), claim);
        return Created("", result);
    }

    [Authorize]
    [HttpPut("{id}")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task <ActionResult> UpdateAccount([FromBody] UpdateAccountRequest accountRequest, int id)
    {
        _logger.LogInformation($"Controller: Update an account by id: {id}. IsDeleted {accountRequest.IsDeleted}, AccountStatus {accountRequest.Status}");
        var claim = this.GetClaims();
        await _accountService.UpdateAccount(_mapper.Map<AccountDto>(accountRequest), id, claim);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task <ActionResult> DeleteAccount(int id)
    {
        _logger.LogInformation($"Controller: Delete an account by id: {id}");
        var claim = this.GetClaims();
        await _accountService.DeleteAccount(id, claim);
        return NoContent();
    }


    // patch for admin
}
