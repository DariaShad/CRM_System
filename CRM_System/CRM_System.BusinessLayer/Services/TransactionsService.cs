using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class TransactionsService : ITransactionsService
{
    private readonly IHttpService _httpService;
    private string _path;
    private readonly ILogger<TransactionsService> _logger;

    public TransactionsService(IHttpService httpService, ILogger<TransactionsService> logger)
    {
        _httpService = httpService;
        _logger= logger;
    }

    public async Task<long> AddDeposit(TransactionRequest request)
    {
        _logger.LogInformation("Business layer: Database query for adding deposit");
        _path = "/transactions/deposit";
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<long> AddWithdraw(TransactionRequest request)
    {
        _logger.LogInformation("Business layer: Database query for adding withdraw");
        _path = "/transactions/withdraw";
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<List<long>> AddTransfer(TransferTransactionRequest request)
    {
        _logger.LogInformation("Business layer: Database query for adding transfer");
        _path = "/transactions/transfer";
        return await _httpService.Post<TransferTransactionRequest, List<long>>(request, _path);
    }

    public async Task<string> GetTransactionById(int transactionId)
    {
        _logger.LogInformation("Business layer: Database query for getting transaction by id");
        return await _httpService.GetTransaction(transactionId);
    }

    public async Task<string> GetTransactionsByAccountId(int accountId)
    {
        _logger.LogInformation("Business layer: Database query for getting transactions by account id");
        return await _httpService.GetTransactionsByAccountId(accountId);
    }

    public async Task<string> GetBalanceByAccountsId(int accountId)
    {
        _logger.LogInformation("Business layer: Database query for getting balance by accounts id");
        return await _httpService.GetBalanceByAccountsId(accountId);
    }
}