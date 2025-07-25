using ModelingEvolution.WixClient.Identifiers;
using ModelingEvolution.WixClient.Models;
using ModelingEvolution.WixClient.Models.Requests;

namespace ModelingEvolution.WixClient.Abstractions;

public interface IBlogClient
{
    // Posts
    Task<BlogPost> GetPostAsync(PostId postId, CancellationToken cancellationToken = default);
    Task<BlogPostList> ListPostsAsync(ListPostsRequest? request = null, CancellationToken cancellationToken = default);
    Task<BlogPost> CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken = default);
    Task<BlogPost> UpdatePostAsync(PostId postId, UpdatePostRequest request, CancellationToken cancellationToken = default);
    Task DeletePostAsync(PostId postId, CancellationToken cancellationToken = default);
    
    // Draft Posts
    Task<DraftPost> CreateDraftPostAsync(CreateDraftPostRequest request, CancellationToken cancellationToken = default);
    Task<DraftPost> GetDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default);
    Task<DraftPost> UpdateDraftPostAsync(DraftPostId draftId, UpdateDraftPostRequest request, CancellationToken cancellationToken = default);
    Task PublishDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default);
    Task DeleteDraftPostAsync(DraftPostId draftId, CancellationToken cancellationToken = default);
    
    // Categories
    Task<CategoryList> ListCategoriesAsync(CancellationToken cancellationToken = default);
    Task<Category> GetCategoryAsync(CategoryId categoryId, CancellationToken cancellationToken = default);
    
    // Tags
    Task<TagList> ListTagsAsync(CancellationToken cancellationToken = default);
}