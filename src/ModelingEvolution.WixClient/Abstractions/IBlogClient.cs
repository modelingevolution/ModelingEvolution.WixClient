using ModelingEvolution.WixClient.Models;
using ModelingEvolution.WixClient.Models.Requests;

namespace ModelingEvolution.WixClient.Abstractions;

public interface IBlogClient
{
    // Posts
    Task<BlogPost> GetPostAsync(string postId, CancellationToken cancellationToken = default);
    Task<BlogPostList> ListPostsAsync(ListPostsRequest? request = null, CancellationToken cancellationToken = default);
    Task<BlogPost> CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken = default);
    Task<BlogPost> UpdatePostAsync(string postId, UpdatePostRequest request, CancellationToken cancellationToken = default);
    Task DeletePostAsync(string postId, CancellationToken cancellationToken = default);
    
    // Draft Posts
    Task<DraftPost> CreateDraftPostAsync(CreateDraftPostRequest request, CancellationToken cancellationToken = default);
    Task<DraftPost> GetDraftPostAsync(string draftId, CancellationToken cancellationToken = default);
    Task<DraftPost> UpdateDraftPostAsync(string draftId, UpdateDraftPostRequest request, CancellationToken cancellationToken = default);
    Task PublishDraftPostAsync(string draftId, CancellationToken cancellationToken = default);
    Task DeleteDraftPostAsync(string draftId, CancellationToken cancellationToken = default);
    
    // Categories
    Task<CategoryList> ListCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Category> GetCategoryAsync(string categoryId, CancellationToken cancellationToken = default);
    
    // Tags
    Task<TagList> ListTagsAsync(CancellationToken cancellationToken = default);
}