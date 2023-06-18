using Application.Dtos.Tag.Responses;

namespace Application.Dtos.Article.Responses;
public record ArticleResponse(
    string Id,
    string Title,
    string Slug,
    string Image,
    string Description,
    string DescriptionHtml,
    List<string> Likes,
    DateTimeOffset PublishedAt,
    List<TagResponse> Tags);
