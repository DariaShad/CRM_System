using CRM_System.BusinessLayer.Services;
using IncredibleBackendContracts.Requests;
using IncredibleBackendContracts.Responses;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class TransactionsService : ITransactionsService
{
    private readonly IHttpService _httpService;
    private readonly ILogger<TransactionsService> _logger;
    public TransactionsService(IHttpService httpService, ILogger<TransactionsService> logger)
    {
        _httpService = httpService;
        _logger= logger;
    }

    public async Task<long> AddDeposit(TransactionRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding deposit: {request.AccountId}, {request.Amount}, {request.Currency}");
        return await _httpService.Post<TransactionRequest, long>(request, PathConst.DepositPath);
    }

    public async Task<long> AddWithdraw(TransactionRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding withdraw {request.AccountId}, {request.Amount}, {request.Currency}");
        return await _httpService.Post<TransactionRequest, long>(request, PathConst.WithdrawPath);
    }

    public async Task<List<long>> AddTransfer(TransactionTransferRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding transfer {request.RecipientAccountId}, {request.AccountId}, {request.Amount}, {request.Currency}");
        return await _httpService.Post<TransactionTransferRequest, List<long>>(request, PathConst.TransferPath);
    }

    public async Task <TransactionResponse> GetTransactionById(int transactionId)
    {
        _logger.LogInformation($"Business layer: Database query for getting transaction by id {transactionId}");
        var content =await _httpService.GetTransaction(transactionId);
        return content;
    }

    public async Task<List<TransactionResponse>> GetTransactionsByAccountId(int accountId)
    {
        _logger.LogInformation($"Business layer: Database query for getting transactions by account id {accountId}");
        var content = await _httpService.GetTransactionsByAccountId(accountId);
        return content;
    }

    public async Task<decimal> GetBalanceByAccountsId(int accountId)
    {
        _logger.LogInformation($"Business layer: Database query for getting balance by accounts id {accountId}");
        return await _httpService.GetBalanceByAccountsId(accountId);
    }
}