# ModelingEvolution.WixClient

A .NET client library for interacting with Wix APIs, focusing on blog management functionality.

## Features

- Blog post management (CRUD operations)
- Draft post creation and management
- Category and tag management
- Strongly-typed request/response models
- Async/await support
- Comprehensive error handling

## Installation

```bash
dotnet add package ModelingEvolution.WixClient
```

## Quick Start

```csharp
using ModelingEvolution.WixClient;
using ModelingEvolution.WixClient.Identifiers;
using Microsoft.Extensions.Logging;

// Initialize the client
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var client = new WixClientBuilder()
    .WithApiKey("your-api-key")
    .WithAccountId("your-account-id")
    .WithLoggerFactory(loggerFactory)
    .Build();

// Get a blog post (using strongly-typed ID)
PostId postId = "post-id";
var post = await client.Blog.GetPostAsync(postId);

// Create a draft post
var draft = await client.Blog.CreateDraftPostAsync(new CreateDraftPostRequest
{
    DraftPost = new DraftPostData
    {
        Title = "My New Post",
        Content = "Post content here...",
        CategoryIds = new List<CategoryId> { "category-1", "category-2" }
    }
});
```

## Configuration

The client can be configured using environment variables or through the builder pattern:

```csharp
var client = new WixClientBuilder()
    .WithApiKey(Environment.GetEnvironmentVariable("WIX_API_KEY"))
    .WithAccountId(Environment.GetEnvironmentVariable("WIX_ACCOUNT_ID"))
    .WithBaseUrl("https://www.wixapis.com") // Optional, uses default
    .Build();
```

## Testing

Run the tests using:

```bash
dotnet test
```

Integration tests require valid Wix API credentials. Set them in your environment or in `appsettings.json`:

```json
{
  "Wix": {
    "ApiKey": "your-api-key",
    "AccountId": "your-account-id"
  }
}
```

## License

MIT