using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BlazorTodoClient.ServiceClients;

public class BlazorTodoApiService
{
    private readonly ILogger<BlazorTodoApiService> _logger;
    private readonly HttpClient _httpClient;

    public BlazorTodoApiService(ILogger<BlazorTodoApiService> logger, HttpClient httpClient) =>
        (_logger, _httpClient) = (logger, httpClient);

    public Task<TResponse?> GetAsync<TResponse>(string path)
    {
        _logger.LogInformation("GET: Retrieving resource of type {Name}", typeof(TResponse).Name);
        return _httpClient.GetFromJsonAsync<TResponse>(path);
    }

    public Task<HttpResponseMessage> PostAsync<TBody>(string path, TBody body)
    {
        _logger.LogInformation("POST: Creating resource of type {Name}", typeof(TBody).Name);
        return _httpClient.PostAsJsonAsync(path, body);
    }

    public Task<HttpResponseMessage> PutAsync<TBody>(string path, TBody body)
    {
        _logger.LogInformation("PUT: Updating resource of type {Name}", typeof(TBody).Name);
        return _httpClient.PutAsJsonAsync(path, body);
    }

    public Task<HttpResponseMessage> DeleteAsync(string path)
    {
        _logger.LogInformation("DELETE: Removing resource");
        return _httpClient.DeleteAsync(path);
    }
}