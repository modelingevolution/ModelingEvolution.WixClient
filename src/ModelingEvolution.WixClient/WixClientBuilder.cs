using ModelingEvolution.WixClient.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace ModelingEvolution.WixClient;

public class WixClientBuilder
{
    private string _apiKey = string.Empty;
    private string _accountId = string.Empty;
    private string _baseUrl = "https://www.wixapis.com";
    private TimeSpan _timeout = TimeSpan.FromSeconds(30);
    private ILoggerFactory _loggerFactory = NullLoggerFactory.Instance;
    private HttpClient? _httpClient;

    public WixClientBuilder WithApiKey(string apiKey)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        return this;
    }

    public WixClientBuilder WithAccountId(string accountId)
    {
        _accountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
        return this;
    }

    public WixClientBuilder WithBaseUrl(string baseUrl)
    {
        _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        return this;
    }

    public WixClientBuilder WithTimeout(TimeSpan timeout)
    {
        _timeout = timeout;
        return this;
    }

    public WixClientBuilder WithLoggerFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        return this;
    }

    public WixClientBuilder WithHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        return this;
    }

    public WixClient Build()
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            throw new InvalidOperationException("API key is required");

        if (string.IsNullOrWhiteSpace(_accountId))
            throw new InvalidOperationException("Account ID is required");

        var configuration = new WixConfiguration
        {
            ApiKey = _apiKey,
            AccountId = _accountId,
            BaseUrl = _baseUrl,
            TimeoutSeconds = (int)_timeout.TotalSeconds
        };

        var httpClient = _httpClient ?? new HttpClient();
        
        return new WixClient(httpClient, configuration, _loggerFactory);
    }
}