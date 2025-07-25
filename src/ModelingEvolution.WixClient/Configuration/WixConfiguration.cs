namespace ModelingEvolution.WixClient.Configuration;

public class WixConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://www.wixapis.com";
    public int TimeoutSeconds { get; set; } = 30;
}