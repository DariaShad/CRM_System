using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;
using CRM_System.API.Validators;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Services.Interfaces;
using DataFiller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountService;
    private readonly IMapper _mapper;

    public AccountsController (IAccountsService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;

    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<AccountResponse> GetAccount(int id)
    {
        var claim = this.GetClaims();
        var result = _accountService.GetAccountById(id, claim);
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
    public ActionResult<List<AccountResponse>> GetAllAccountsByLeadId(int leadId)
    {
        var claim = this.GetClaims();
        var result = _accountService.GetAllAccountsByLeadId(leadId, claim);
        return Ok(_mapper.Map<List<AccountResponse>>(result));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> AddAccount([FromBody] AddAccountRequest accountRequest)
    {
        // add validation 
        var claim = this.GetClaims();
        var result=_accountService.AddAccount(_mapper.Map<AccountDto>(accountRequest), claim);
        return Created("", result);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPut] // add id
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult UpdateAccount([FromBody] UpdateAccountRequest accountRequest, int id)
    {
        // add validation 

        var claim = this.GetClaims();
        _accountService.UpdateAccount(_mapper.Map<AccountDto>(accountRequest), id, claim);
        return NoContent();
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpDelete] // add id
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult DeleteAccount(int id)
    {
        var claim = this.GetClaims();
        _accountService.DeleteAccount(id, claim);
        return NoContent();
    }


    // patch for admin
}
