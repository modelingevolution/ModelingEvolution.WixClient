# Wix API Setup Guide

This guide will help you obtain the necessary credentials to use the WixClient.

## Prerequisites

1. A Wix account with a website
2. Access to Wix Developer Center

## Getting Your API Credentials

### 1. Create a Wix App

1. Go to [Wix Developers](https://dev.wix.com/)
2. Click "Create New App"
3. Choose "Website / Business App"
4. Fill in your app details:
   - App Name: Your application name
   - App Description: Brief description
   - Website URL: Your website URL

### 2. Get Your API Key (OAuth App)

1. In your app dashboard, go to "OAuth" section
2. Click "Create New OAuth App" if needed
3. Set redirect URLs (for server-to-server, you can use `https://localhost`)
4. Copy your:
   - **App ID** (Client ID)
   - **App Secret** (Client Secret)

### 3. Generate an Access Token

For server-to-server authentication, you'll need to:

1. Use the OAuth flow to get an access token
2. Or use Wix's API Key feature (if available for your app type)

#### Option A: API Key (Recommended for Blog API)

1. Go to your Wix site dashboard
2. Navigate to "Settings" > "API Keys"
3. Create a new API key with Blog permissions:
   - Read Blog
   - Manage Blog
4. Copy the generated API key

#### Option B: OAuth Access Token

```bash
# Get authorization code
https://www.wix.com/installer/install?appId=YOUR_APP_ID&redirectUrl=YOUR_REDIRECT_URL

# Exchange for access token
curl -X POST https://www.wixapis.com/oauth/access \
  -H "Content-Type: application/json" \
  -d '{
    "grant_type": "authorization_code",
    "client_id": "YOUR_APP_ID",
    "client_secret": "YOUR_APP_SECRET",
    "code": "AUTHORIZATION_CODE"
  }'
```

### 4. Get Your Account ID

1. Log into your Wix account
2. Go to your site's dashboard
3. The Account ID is typically in the URL or in your account settings
4. It might also be called "Site ID" or "Instance ID"

## Using Credentials with WixClient

### For Testing

Create `appsettings.local.json` in the tests folder:

```json
{
  "Wix": {
    "ApiKey": "YOUR_API_KEY_OR_ACCESS_TOKEN",
    "AccountId": "YOUR_ACCOUNT_ID"
  }
}
```

### For Production

Use environment variables:

```bash
export Wix__ApiKey="YOUR_API_KEY_OR_ACCESS_TOKEN"
export Wix__AccountId="YOUR_ACCOUNT_ID"
```

Or in code:

```csharp
var client = new WixClientBuilder()
    .WithApiKey("YOUR_API_KEY_OR_ACCESS_TOKEN")
    .WithAccountId("YOUR_ACCOUNT_ID")
    .WithLoggerFactory(loggerFactory)
    .Build();
```

## Required Permissions

For the Blog API, ensure your API key or OAuth app has these permissions:

- **Read Blog** - Required for reading posts, categories, tags
- **Manage Blog** - Required for creating, updating, deleting posts

## Troubleshooting

### 401 Unauthorized
- Check that your API key is valid
- Ensure the key has the necessary permissions
- Verify the Account ID is correct

### 403 Forbidden
- The API key might not have sufficient permissions
- Check if the blog feature is enabled on your Wix site

### 404 Not Found
- Verify the Account ID is correct
- Ensure you're accessing the right site/blog

## Security Best Practices

1. **Never commit credentials** to source control
2. Use environment variables or secure key vaults in production
3. Rotate API keys regularly
4. Use least-privilege principle - only request needed permissions
5. Monitor API usage for unusual activity

## Additional Resources

- [Wix REST API Documentation](https://dev.wix.com/docs/rest)
- [Blog API Reference](https://dev.wix.com/docs/rest/business-solutions/blog)
- [Authentication Guide](https://dev.wix.com/docs/build-apps/develop-your-app/api-integrations/authentication)