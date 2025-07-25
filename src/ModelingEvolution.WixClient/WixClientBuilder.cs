using ModelingEvolution.WixClient.Configuration;

namespace ModelingEvolution.WixClient;

public class WixClientBuilder
{
    private readonly WixConfiguration _configuration = new();
    private HttpClient? _httpClient;

    public WixClientBuilder WithApiKey(string apiKey)
    {
        _configuration.ApiKey = apiKey;
        return this;
    }

    public WixClientBuilder WithAccountId(string accountId)
    {
        _configuration.AccountId = accountId;
        return this;
    }

    public WixClientBuilder WithBaseUrl(string baseUrl)
    {
        _configuration.BaseUrl = baseUrl;
        return this;
    }

    public WixClientBuilder WithTimeout(int timeoutSeconds)
    {
        _configuration.TimeoutSeconds = timeoutSeconds;
        return this;
    }

    public WixClientBuilder WithHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        return this;
    }

    public IWixClient Build()
    {
        if (string.IsNullOrEmpty(_configuration.ApiKey))
        {
            throw new InvalidOperationException("API key is required. Use WithApiKey() to set it.");
        }

        if (string.IsNullOrEmpty(_configuration.AccountId))
        {
            throw new InvalidOperationException("Account ID is required. Use WithAccountId() to set it.");
        }

        var httpClient = _httpClient ?? new HttpClient();
        return new WixClient(_configuration, httpClient);
    }
}