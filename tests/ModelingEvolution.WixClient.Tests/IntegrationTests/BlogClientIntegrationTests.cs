using FluentAssertions;
using ModelingEvolution.WixClient.Models.Requests;
using Xunit;

namespace ModelingEvolution.WixClient.Tests.IntegrationTests;

[Trait("Category", "Integration")]
public class BlogClientIntegrationTests : IntegrationTestBase
{
    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task GetPost_WithValidId_ReturnsPost()
    {
        // Arrange
        var postId = "test-post-id"; // Replace with actual post ID

        // Act
        var post = await Client.Blog.GetPostAsync(postId);

        // Assert
        post.Should().NotBeNull();
        post.Id.Should().Be(postId);
        post.Title.Should().NotBeNullOrEmpty();
    }

    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task ListPosts_ReturnsPostList()
    {
        // Act
        var result = await Client.Blog.ListPostsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Posts.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task CreateDraftPost_WithValidData_CreatesDraft()
    {
        // Arrange
        var request = new CreateDraftPostRequest
        {
            DraftPost = new DraftPostData
            {
                Title = $"Test Draft Post {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
                Content = "This is a test draft post created by integration tests.",
                Excerpt = "Test excerpt",
                CommentingEnabled = true
            }
        };

        // Act
        var draft = await Client.Blog.CreateDraftPostAsync(request);

        // Assert
        draft.Should().NotBeNull();
        draft.Title.Should().Be(request.DraftPost.Title);
        draft.Content.Should().Be(request.DraftPost.Content);
        draft.Status.Should().Be("DRAFT");

        // Cleanup
        await Client.Blog.DeleteDraftPostAsync(draft.Id);
    }

    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task UpdateDraftPost_WithValidData_UpdatesDraft()
    {
        // Arrange - Create a draft first
        var createRequest = new CreateDraftPostRequest
        {
            DraftPost = new DraftPostData
            {
                Title = "Original Title",
                Content = "Original content"
            }
        };
        var draft = await Client.Blog.CreateDraftPostAsync(createRequest);

        // Act - Update the draft
        var updateRequest = new UpdateDraftPostRequest
        {
            DraftPost = new DraftPostData
            {
                Title = "Updated Title",
                Content = "Updated content"
            },
            FieldMask = new FieldMask
            {
                Paths = new List<string> { "title", "content" }
            }
        };
        var updatedDraft = await Client.Blog.UpdateDraftPostAsync(draft.Id, updateRequest);

        // Assert
        updatedDraft.Should().NotBeNull();
        updatedDraft.Title.Should().Be("Updated Title");
        updatedDraft.Content.Should().Be("Updated content");

        // Cleanup
        await Client.Blog.DeleteDraftPostAsync(draft.Id);
    }

    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task ListCategories_ReturnsCategories()
    {
        // Act
        var result = await Client.Blog.ListCategoriesAsync();

        // Assert
        result.Should().NotBeNull();
        result.Categories.Should().NotBeNull();
    }

    [Fact(Skip = "Requires valid Wix API credentials")]
    public async Task ListTags_ReturnsTags()
    {
        // Act
        var result = await Client.Blog.ListTagsAsync();

        // Assert
        result.Should().NotBeNull();
        result.Tags.Should().NotBeNull();
    }
}