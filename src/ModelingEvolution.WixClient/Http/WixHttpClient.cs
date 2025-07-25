using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ModelingEvolution.WixClient.Configuration;
using Microsoft.Extensions.Logging;

namespace ModelingEvolution.WixClient.Http;

public class WixHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WixHttpClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public WixHttpClient(HttpClient httpClient, WixConfiguration configuration, ILogger<WixHttpClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        ConfigureHttpClient(configuration);
    }

    private void ConfigureHttpClient(WixConfiguration configuration)
    {
        _httpClient.BaseAddress = new Uri(configuration.BaseUrl.EndsWith('/') ? configuration.BaseUrl : configuration.BaseUrl + "/");
        _httpClient.Timeout = TimeSpan.FromSeconds(configuration.TimeoutSeconds);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.ApiKey}");
        _httpClient.DefaultRequestHeaders.Add("wix-account-id", configuration.AccountId);
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        var fullUrl = new Uri(_httpClient.BaseAddress!, endpoint).ToString();
        _logger.LogInformation("GET {FullUrl} (BaseAddress: {BaseAddress}, Endpoint: {Endpoint})", fullUrl, _httpClient.BaseAddress, endpoint);
        
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Request failed with status {StatusCode}: {Content}", response.StatusCode, content);
        }
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogTrace("Response: {Content}", responseContent);
        
        var result = JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        return result ?? throw new InvalidOperationException($"Failed to deserialize response to {typeof(T).Name}");
    }

    public async Task<T> PostAsync<T>(string endpoint, object request, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        _logger.LogInformation("POST {Endpoint} with body: {Body}", endpoint, json);
        
        var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Request failed with status {StatusCode}: {Content}", response.StatusCode, errorContent);
        }
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogTrace("Response: {Content}", responseContent);
        
        var result = JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        return result ?? throw new InvalidOperationException($"Failed to deserialize response to {typeof(T).Name}");
    }

    public async Task<T> PatchAsync<T>(string endpoint, object request, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        _logger.LogInformation("PATCH {Endpoint} with body: {Body}", endpoint, json);
        
        var httpRequest = new HttpRequestMessage(HttpMethod.Patch, endpoint)
        {
            Content = content
        };
        
        var response = await _httpClient.SendAsync(httpRequest, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Request failed with status {StatusCode}: {Content}", response.StatusCode, errorContent);
        }
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogTrace("Response: {Content}", responseContent);
        
        var result = JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        return result ?? throw new InvalidOperationException($"Failed to deserialize response to {typeof(T).Name}");
    }

    public async Task<T> PutAsync<T>(string endpoint, object request, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(request, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        _logger.LogInformation("PUT {Endpoint} with body: {Body}", endpoint, json);
        
        var response = await _httpClient.PutAsync(endpoint, content, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Request failed with status {StatusCode}: {Content}", response.StatusCode, errorContent);
        }
        
        response.EnsureSuccessStatusCode();
        
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogTrace("Response: {Content}", responseContent);
        
        var result = JsonSerializer.Deserialize<T>(responseContent, _jsonOptions);
        return result ?? throw new InvalidOperationException($"Failed to deserialize response to {typeof(T).Name}");
    }
    
    public async Task DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("DELETE {Endpoint}", endpoint);
        
        var response = await _httpClient.DeleteAsync(endpoint, cancellationToken);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError("Request failed with status {StatusCode}: {Content}", response.StatusCode, errorContent);
        }
        
        response.EnsureSuccessStatusCode();
    }
}