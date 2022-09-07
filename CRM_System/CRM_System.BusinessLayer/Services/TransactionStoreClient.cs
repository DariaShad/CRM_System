using System.Text;
using System.Text.Json;

namespace CRM_System.BusinessLayer;

public class TransactionStoreClient : IHttpService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;
    public const string Accounts = "accounts";

    public TransactionStoreClient()
    {
        string baseAddress ="https://piter-education.ru:6060/";

        if (_httpClient.BaseAddress == null)
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        //_httpClient.Timeout = new TimeSpan(0, 0, 30);

        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<K> Post<T, K>(T payload, string path)
    {
        var serializedPayload = JsonSerializer.Serialize(payload);
        var requestPayload = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(path, requestPayload);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<K>(content, _options);
        return result;
    }

    public async Task<string> GetTransaction(int transactionId)
    {
        var response = await _httpClient.GetAsync($"transactions/{transactionId}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
    public async Task<string> GetTransactionsByAccountId(int accountId)
    {
        var response = await _httpClient.GetAsync($"accounts/{accountId}/transactions");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> GetBalanceByAccountsId(int accountId)
    {
        var response = await _httpClient.GetAsync($"accounts/{accountId}/balance");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}
