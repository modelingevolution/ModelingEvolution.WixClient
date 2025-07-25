using ModelingEvolution.WixClient.Abstractions;
using ModelingEvolution.WixClient.Http;
using ModelingEvolution.WixClient.Models;
using ModelingEvolution.WixClient.Models.Requests;

namespace ModelingEvolution.WixClient.Clients;

public class BlogClient : IBlogClient
{
    private readonly WixHttpClient _httpClient;

    public BlogClient(WixHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Posts
    public async Task<BlogPost> GetPostAsync(string postId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync<GetPostResponse>($"/blog/v3/posts/{postId}", cancellationToken);
        return response.Post;
    }

    public async Task<BlogPostList> ListPostsAsync(ListPostsRequest? request = null, CancellationToken cancellationToken = default)
    {
        if (request == null)
        {
            return await _httpClient.GetAsync<BlogPostList>("/blog/v3/posts", cancellationToken);
        }

        return await _httpClient.PostAsync<BlogPostList>("/blog/v3/posts/query", request, cancellationToken);
    }

    public async Task<BlogPost> CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync<GetPostResponse>("/blog/v3/posts", request, cancellationToken);
        return response.Post;
    }

    public async Task<BlogPost> UpdatePostAsync(string postId, UpdatePostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsync<GetPostResponse>($"/blog/v3/posts/{postId}", request, cancellationToken);
        return response.Post;
    }

    public async Task DeletePostAsync(string postId, CancellationToken cancellationToken = default)
    {
        await _httpClient.DeleteAsync($"/blog/v3/posts/{postId}", cancellationToken);
    }

    // Draft Posts
    public async Task<DraftPost> CreateDraftPostAsync(CreateDraftPostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsync<GetDraftPostResponse>("/blog/v3/draft-posts", request, cancellationToken);
        return response.DraftPost;
    }

    public async Task<DraftPost> GetDraftPostAsync(string draftId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync<GetDraftPostResponse>($"/blog/v3/draft-posts/{draftId}", cancellationToken);
        return response.DraftPost;
    }

    public async Task<DraftPost> UpdateDraftPostAsync(string draftId, UpdateDraftPostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PatchAsync<GetDraftPostResponse>($"/blog/v3/draft-posts/{draftId}", request, cancellationToken);
        return response.DraftPost;
    }

    public async Task PublishDraftPostAsync(string draftId, CancellationToken cancellationToken = default)
    {
        await _httpClient.PostAsync<object>($"/blog/v3/draft-posts/{draftId}/publish", new { }, cancellationToken);
    }

    public async Task DeleteDraftPostAsync(string draftId, CancellationToken cancellationToken = default)
    {
        await _httpClient.DeleteAsync($"/blog/v3/draft-posts/{draftId}", cancellationToken);
    }

    // Categories
    public async Task<CategoryList> ListCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetAsync<CategoryList>("/blog/v3/categories", cancellationToken);
    }

    public async Task<Category> GetCategoryAsync(string categoryId, CancellationToken cancellationToken = default)
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