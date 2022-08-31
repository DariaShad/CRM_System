namespace CRM_System.BusinessLayer;

public class TransactionsService : ITransactionsService
{
    private readonly IHttpService _httpService;
    private string _path;

    public TransactionsService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<long> AddDeposit(TransactionRequest request)
    {
        _path = "deposit";
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<long> AddWithdraw(TransactionRequest request)
    {
        _path = "withdraw";
        return await _httpService.Post<TransactionRequest, long>(request, _path);
    }

    public async Task<List<long>> AddTransfer(TransferTransactionRequest request)
    {
        _path = "transfer";
        return await _httpService.Post<TransferTransactionRequest, List<long>>(request, _path);
    }

    public async Task<string> GetTransactionsByAccountId(int accountId)
    {
        return await _httpService.GetTransactions(accountId);
    }
}