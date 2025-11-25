using System.Net.Http.Json;

namespace KYAPI.Services;

public class HttpService : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var client = _httpClientFactory.CreateClient();
        return await client.GetFromJsonAsync<T>(url);
    }

    public async Task<T?> PostAsync<T>(string url, object data)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        
        // If T is string, read as string
        if (typeof(T) == typeof(string))
        {
             return (T)(object)await response.Content.ReadAsStringAsync();
        }

        // Otherwise try to deserialize from JSON (if content exists)
        if (response.Content.Headers.ContentLength == 0)
        {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<T>();
    }
}
