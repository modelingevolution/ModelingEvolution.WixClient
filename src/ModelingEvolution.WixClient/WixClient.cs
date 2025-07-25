using ModelingEvolution.WixClient.Abstractions;
using ModelingEvolution.WixClient.Clients;
using ModelingEvolution.WixClient.Configuration;
using ModelingEvolution.WixClient.Http;

namespace ModelingEvolution.WixClient;

public class WixClient : IWixClient
{
    public IBlogClient Blog { get; }

    public WixClient(WixConfiguration configuration, HttpClient httpClient)
    {
        var wixHttpClient = new WixHttpClient(httpClient, configuration);
        Blog = new BlogClient(wixHttpClient);
    }
}