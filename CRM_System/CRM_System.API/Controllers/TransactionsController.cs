﻿using CRM.DataLayer;
using CRM_System.BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class TransactionsController : Controller
{
    public ClaimModel _claims;
    private readonly ITransactionsService _transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPost("deposit")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<long>> AddDeposit([FromBody] TransactionRequest request)
    {
        var claims = this.GetClaims();
        var transactionId = await _transactionsService.AddDeposit(request);
        return Created($"{this.GetShemeAndHostString()}/transactions/{transactionId}", transactionId);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPost("withdraw")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<long>> AddWithdraw([FromBody] TransactionRequest request)
    {
        var claims = this.GetClaims();
        var transactionId = await _transactionsService.AddWithdraw(request);
        return Created($"{this.GetShemeAndHostString()}/transactions/{transactionId}", transactionId);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPost("transfer")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<List<long>>> AddTransfer([FromBody] TransferTransactionRequest request)
    {
        var claims = this.GetClaims();
        var transactionId = await _transactionsService.AddTransfer(request);
        return Created($"{this.GetShemeAndHostString()}/transactions/{transactionId}", transactionId);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip, Role.Admin)]
    [HttpGet("{accountId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> GetTransactionsByAccountId(int accountId)
    {
        var claims = this.GetClaims();
        var transactions = await _transactionsService.GetTransactionsByAccountId(accountId);
        return Json(transactions);
    }
}
