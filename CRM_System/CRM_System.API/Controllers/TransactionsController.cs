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
    private readonly ILogger<TransactionsController> _logger;

    public TransactionsController(ITransactionsService transactionsService, ILogger<TransactionsController> logger)
    {
        _transactionsService = transactionsService;
        _logger = logger;
    }

    [Authorize]
    [HttpPost("deposit/tr")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<long>> AddDeposit([FromBody] TransactionRequest request)
    {
        _logger.LogInformation("Controllers: Add deposit");
        var claims = this.GetClaims();
        var transactionId = await _transactionsService.AddDeposit(request);
        return Created($"{this.GetShemeAndHostString()}/transactions/{transactionId}", transactionId);
    }

    [Authorize]
    [HttpPost("tr/withdraw")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<long>> AddWithdraw([FromBody] TransactionRequest request)
    {
        _logger.LogInformation("Controllers: Add withdraw");
        var claims = this.GetClaims();
        var transactionId = await _transactionsService.AddWithdraw(request);
        return Created($"{this.GetShemeAndHostString()}/transactions/{transactionId}", transactionId);
    }

    [Authorize]
    [HttpPost("sf/transfer/gd")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<List<long>>> AddTransfer([FromBody] TransferTransactionRequest request)
    {
        _logger.LogInformation("Controllers: Add transfer");
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
        _logger.LogInformation("Controllers: Get transaction by id");
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
        _logger.LogInformation("Controllers: Get transaction by account id");
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
        _logger.LogInformation("Controllers: Get balance by account id");
        var claims = this.GetClaims();
        var transactions = await _transactionsService.GetBalanceByAccountsId(accountId);
        return Json(transactions);
    }
}
