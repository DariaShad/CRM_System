namespace CRM_System.BusinessLayer;

public interface IHttpService
{
    Task<K> Post<T, K>(T payload, string path);
    Task<string> GetTransactions(int accoundId);
}
