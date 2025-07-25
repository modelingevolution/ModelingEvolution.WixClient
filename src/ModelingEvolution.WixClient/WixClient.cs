using ModelingEvolution.WixClient.Abstractions;
using ModelingEvolution.WixClient.Clients;
using ModelingEvolution.WixClient.Configuration;
using ModelingEvolution.WixClient.Http;
using Microsoft.Extensions.Logging;

namespace ModelingEvolution.WixClient;

public class WixClient : IWixClient
{
    public IBlogClient Blog { get; }

    public WixClient(
        HttpClient httpClient,
        WixConfiguration configuration,
        ILoggerFactory loggerFactory)
    {
        if (httpClient == null) throw new ArgumentNullException(nameof(httpClient));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
        
        var wixHttpClient = new WixHttpClient(
            httpClient,
            configuration,
            loggerFactory.CreateLogger<WixHttpClient>());

        Blog = new BlogClient(wixHttpClient, loggerFactory.CreateLogger<BlogClient>());
    }
}