using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;
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
    public ClaimModel Claims;

    public AccountsController (IAccountsService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;

    }

    //[AuthorizeByRole(Role.Regular, Role.Vip, Role.Admin)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<AccountResponse> GetAccount(int id)
    {
        CsvCreater.FillListOfLeads();
        CsvCreater.BulkInsertLeads();

        //CsvCreater.FillListOfAccounts();
        //CsvCreater.BulkInsertAccounts();
        return Ok(new AccountResponse { Id = id });
        //var claim = this.GetClaims();
        //var result = _accountService.GetAccountById(id, claim);
        //if (result == null)
        //    return NotFound();
        //else
        //    return Ok(_mapper.Map<AccountResponse>(result));
    }


    [AuthorizeByRole(Role.Admin)]
    [HttpGet]
    [ProducesResponseType(typeof(AllAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult <List<AllAccountsResponse>> GetAllAccounts()
    {
        var result = _accountService.GetAllAccounts();
        return Ok(_mapper.Map<List<AllAccountsResponse>>(result));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip, Role.Admin)]
    [HttpGet("{id}/lead")]
    [ProducesResponseType(typeof(AllAccountsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult<List<AllAccountsResponse>> GetAllAccountsByLeadId(int leadId)
    {
        var claim = this.GetClaims();
        var result = _accountService.GetAllAccountsByLeadId(leadId, claim);
        return Ok(_mapper.Map<List<AllAccountsResponse>>(result));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult <int> AddAccount([FromBody] AddAccountRequest accountRequest)
    {
        var claim = this.GetClaims();
        var result=_accountService.AddAccount(_mapper.Map<AccountDto>(accountRequest), claim);
        return Created("", result);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult UpdateAccount([FromBody] UpdateAccountRequest accountRequest, int id)
    {
        var claim = this.GetClaims();
        _accountService.UpdateAccount(_mapper.Map<AccountDto>(accountRequest), id, claim);
        return NoContent();
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public ActionResult DeleteAccount(int id)
    {
        var claim = this.GetClaims();
        _accountService.DeleteAccount(id, claim);
        return NoContent();
    }
}
