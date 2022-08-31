namespace CRM_System.BusinessLayer;

public interface ITransactionsService
{
    Task<long> AddDeposit(TransactionRequest request);
    Task<long> AddWithdraw(TransactionRequest request);
    Task<List<long>> AddTransfer(TransferTransactionRequest request);
    Task<string> GetTransactionsByAccountId(int accountId);
}
