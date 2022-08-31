using System.Text;
using System.Text.Json;

namespace CRM_System.BusinessLayer;

public class HttpService : IHttpService
{
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly JsonSerializerOptions _options;

    public HttpService()
    {
        _httpClient.BaseAddress = new Uri("https://piter-education.ru:6060/swagger");
        _httpClient.Timeout = new TimeSpan(0, 0, 30);

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

    public async Task<string> GetTransactions(int accoundId)
    {
        var response = await _httpClient.GetAsync("accountId");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}
