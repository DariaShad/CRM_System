using CRM_System.BusinessLayer.Services;
using IncredibleBackendContracts.Requests;
using IncredibleBackendContracts.Responses;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CRM_System.BusinessLayer;

public class TransactionsService : ITransactionsService
{
    private readonly IHttpService _httpService;
    private string _path;
    private readonly ILogger<TransactionsService> _logger;
    private readonly JsonSerializerOptions _options;

    public TransactionsService(IHttpService httpService, ILogger<TransactionsService> logger)
    {
        _httpService = httpService;
        _logger= logger;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<long> AddDeposit(TransactionRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding deposit: {request.AccountId}, {request.Amount}, {request.Currency}");
        _path = PathConst.DepositPath;
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<long> AddWithdraw(TransactionRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding withdraw {request.AccountId}, {request.Amount}, {request.Currency}");
        _path = PathConst.WithdrawPath;
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<List<long>> AddTransfer(TransactionTransferRequest request)
    {
        _logger.LogInformation($"Business layer: Database query for adding transfer {request.RecipientAccountId}, {request.AccountId}, {request.Amount}, {request.Currency}");
        _path = PathConst.TransferPath;
        return await _httpService.Post<TransactionTransferRequest, List<long>>(request, _path);
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