using ModelingEvolution.WixClient.Abstractions;
using ModelingEvolution.WixClient.Http;
using ModelingEvolution.WixClient.Identifiers;
using ModelingEvolution.WixClient.Models;
using ModelingEvolution.WixClient.Models.Requests;
using Microsoft.Extensions.Logging;

namespace ModelingEvolution.WixClient.Clients;

public class BlogClient : IBlogClient
{
    private readonly WixHttpClient _httpClient;
    private readonly ILogger<BlogClient> _logger;

    public BlogClient(WixHttpClient httpClient, ILogger<BlogClient> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // Posts
    public async Task<BlogPost> GetPostAsync(PostId postId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting post {PostId}", postId);
        
        var response = await _httpClient.GetAsync<GetPostResponse>($"/blog/v3/posts/{postId}", cancellationToken);
        
        _logger.LogInformation("Retrieved post: {PostTitle}", response.Post.Title);
        return response.Post;
    }

    public async Task<BlogPostList> ListPostsAsync(ListPostsRequest? request = null, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Listing posts");
        
        BlogPostList result;
        if (request == null)
        {
            result = await _httpClient.GetAsync<BlogPostList>("/blog/v3/posts", cancellationToken);
        }
        else
        {
            result = await _httpClient.PostAsync<BlogPostList>("/blog/v3/posts/query", request, cancellationToken);
        }
        
        _logger.LogInformation("Retrieved {Count} posts", result.Posts.Count);
        return result;
    }

    public async Task<BlogPost> CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
            
        _logger.LogInformation("Creating post '{PostTitle}'", request.Post.Title);
        
        var response = await _httpClient.PostAsync<GetPostResponse>("/blog/v3/posts", request, cancellationToken);
        
        _logger.LogInformation("Created post: {PostTitle} (ID: {PostId})", response.Post.Title, response.Post.Id);
        return response.Post;
    }

    public async Task<BlogPost> UpdatePostAsync(PostId postId, UpdatePostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsync<GetPostResponse>($"/blog/v3/posts/{postId}", request, cancellationToken);
        return response.Post;
    }

    public async Task DeletePostAsync(PostId postId, CancellationToken cancellationToken = default)
    {
        await _httpClient.DeleteAsync($"/blog/v3/posts/{postId}", cancellationToken);
    }

    // Draft Posts
    public async Task<DraftPost> CreateDraftPostAsync(CreateDraftPostRequest request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
            
        _logger.LogInformation("Creating draft post '{PostTitle}'", request.DraftPost.Title);
        
        var response = await _httpClient.PostAsync<GetDraftPostResponse>("/blog/v3/draft-posts", request, cancellationToken);
        
        _logger.LogInformation("Created draft post: {PostTitle} (ID: {PostId})", response.DraftPost.Title, response.DraftPost.Id);
        return response.DraftPost;
    }

    public async Task<DraftPost> GetDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync<GetDraftPostResponse>($"/blog/v3/draft-posts/{draftId}", cancellationToken);
        return response.DraftPost;
    }

    public async Task<DraftPost> UpdateDraftPostAsync(DraftPostId draftId, UpdateDraftPostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsync<GetDraftPostResponse>($"/blog/v3/draft-posts/{draftId}", request, cancellationToken);
        return response.DraftPost;
    }

    public async Task PublishDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default)
    {
        await _httpClient.PostAsync<object>($"/blog/v3/draft-posts/{draftId}/publish", new { }, cancellationToken);
    }

    public async Task DeleteDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default)
    {
        await _httpClient.DeleteAsync($"/blog/v3/draft-posts/{draftId}", cancellationToken);
    }

    // Categories
    public async Task<CategoryList> ListCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<CategoryList>("/blog/v3/categories", cancellationToken);
    }

    public async Task<Category> GetCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync<GetCategoryResponse>($"/blog/v3/categories/{categoryId}", cancellationToken);
        return response.Category;
    }

    // Tags
    public async Task<TagList> ListTagsAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<TagList>("/blog/v3/tags", cancellationToken);
    }

    // Response wrapper classes
    private class GetPostResponse
    {
        public BlogPost Post { get; set; } = new();
    }

    private class GetDraftPostResponse
    {
        public DraftPost DraftPost { get; set; } = new();
    }

    private class GetCategoryResponse
    {
        public Category Category { get; set; } = new();
    }
}