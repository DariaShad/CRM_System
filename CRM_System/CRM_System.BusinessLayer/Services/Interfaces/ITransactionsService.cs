using IncredibleBackendContracts.Requests;
using IncredibleBackendContracts.Responses;

namespace CRM_System.BusinessLayer;

public interface ITransactionsService
{
    Task<long> AddDeposit(TransactionRequest request);
    Task<long> AddWithdraw(TransactionRequest request);
    Task<List<long>> AddTransfer(TransactionTransferRequest request);
    Task<TransactionResponse> GetTransactionById(int transactionId);
    Task<List<TransactionResponse>> GetTransactionsByAccountId(int accountId);
    Task<decimal> GetBalanceByAccountsId(int accountId);
}
