using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public AccountsController (IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;

    }

    [HttpGet]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<AccountResponse> GetAccount(int id)
    {
        var result = _accountService.GetAccountById(id);
        if (result == null)
            return NotFound();
        else
            return Ok(_mapper.Map<AccountResponse>(result));
    }

    [HttpGet]
    [ProducesResponseType(typeof(AllAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult <List<AllAccountsResponse>> GetAllAccounts()
    {
        var result = _accountService.GetAllAccounts();
        return Ok(_mapper.Map<List<AllAccountsResponse>>(result));
    }

    [HttpGet]
    [ProducesResponseType(typeof(AllAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult<List<AllAccountsResponse>> GetAllAccountsByLeadId(int leadId)
    {
        var result = _accountService.GetAllAccountsByLeadId(leadId);
        return Ok(_mapper.Map<List<AllAccountsResponse>>(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> AddAccount([FromBody] AddAccountRequest accountRequest)
    {
        var result=_accountService.AddAccount(_mapper.Map<AccountDto>(accountRequest));
        return Created("", result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult UpdateAccount([FromBody] UpdateAccountRequest accountRequest)
    {
        _accountService.UpdateAccount(_mapper.Map<AccountDto>(accountRequest));
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult DeleteAccount(int id)
    {
        _accountService.DeleteAccount(id);
        return NoContent();
    }
}
