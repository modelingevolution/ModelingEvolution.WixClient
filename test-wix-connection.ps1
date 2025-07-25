#!/usr/bin/env pwsh

Write-Host "Testing Wix API Connection..." -ForegroundColor Green

# Check for credentials
$apiKey = $env:Wix__ApiKey
$accountId = $env:Wix__AccountId

if (-not $apiKey) {
    Write-Host "Wix API Key not found in environment variables." -ForegroundColor Yellow
    $apiKey = Read-Host "Enter your Wix API Key"
}

if (-not $accountId) {
    Write-Host "Wix Account ID not found in environment variables." -ForegroundColor Yellow
    $accountId = Read-Host "Enter your Wix Account ID"
}

# Create a simple test
$testCode = @"
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModelingEvolution.WixClient;

class Program
{
    static async Task Main()
    {
        var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        
        try
        {
            var client = new WixClientBuilder()
                .WithApiKey("$apiKey")
                .WithAccountId("$accountId")
                .WithLoggerFactory(loggerFactory)
                .Build();
            
            Console.WriteLine("Client created successfully!");
            
            // Try to list categories
            var categories = await client.Blog.ListCategoriesAsync();
            Console.WriteLine(`$"Found {categories.Categories.Count} categories");
            
            // Try to list tags
            var tags = await client.Blog.ListTagsAsync();
            Console.WriteLine(`$"Found {tags.Tags.Count} tags");
            
            Console.WriteLine("Connection test successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(`$"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
"@

# Save and run the test
$testFile = "test-connection.csx"
$testCode | Out-File -FilePath $testFile -Encoding UTF8

Write-Host "`nRunning connection test..." -ForegroundColor Cyan
dotnet script $testFile

# Cleanup
Remove-Item $testFile -ErrorAction SilentlyContinue

Write-Host "`nTo run full integration tests, use:" -ForegroundColor Green
Write-Host "  .\run-tests.ps1" -ForegroundColor Yellow