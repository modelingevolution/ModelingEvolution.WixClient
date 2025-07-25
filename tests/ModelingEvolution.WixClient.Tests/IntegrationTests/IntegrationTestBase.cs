using Microsoft.Extensions.Configuration;
using ModelingEvolution.WixClient.Abstractions;

namespace ModelingEvolution.WixClient.Tests.IntegrationTests;

public abstract class IntegrationTestBase : IDisposable
{
    protected IWixClient Client { get; }
    protected IConfiguration Configuration { get; }

    protected IntegrationTestBase()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.local.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var apiKey = Configuration["Wix:ApiKey"];
        var accountId = Configuration["Wix:AccountId"];

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(accountId))
        {
            throw new InvalidOperationException(
                "Wix API credentials not configured. Set them in appsettings.json or environment variables.");
        }

        Client = new WixClientBuilder()
            .WithApiKey(apiKey)
            .WithAccountId(accountId)
            .Build();
    }

    public virtual void Dispose()
    {
        // Cleanup if needed
    }
}