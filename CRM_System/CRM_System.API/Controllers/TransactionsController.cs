using CRM_System.BusinessLayer;
using CRM_System.DataLayer;
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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

    [Authorize]
    [HttpGet("{transactionId}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> GetTransactionById(int transactionId)
    {
        var claims = this.GetClaims();
        var transactions = await _transactionsService.GetTransactionById(transactionId);
        return Json(transactions);
    }

    [Authorize]
    [HttpGet("/byAccountId{accountId}")]
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

    [Authorize]
    [HttpGet("/accounts{accountId}/balance")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> GetBalanceByAccountsId(int accountId)
    {
        var claims = this.GetClaims();
        var transactions = await _transactionsService.GetBalanceByAccountsId(accountId);
        return Json(transactions);
    }
}
