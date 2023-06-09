using Application.Dtos.Tag.Responses;

namespace Application.Dtos.Article.Responses;
public record ArticleResponse(
    string Id,
    string Title,
    string Slug,
    string Image,
    string Description,
    string DescriptionHtml,
    int Likes,
    List<TagResponse> Tags);
