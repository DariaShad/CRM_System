namespace CRM_System.BusinessLayer;

public interface IHttpService
{
    Task<K> Post<T, K>(T payload, string path);
    Task<string> GetTransaction(int transactionId);
    Task <string> GetTransactionsByAccountId (int accountId);

    Task<string> GetBalanceByAccountsId(int accountId);
}
