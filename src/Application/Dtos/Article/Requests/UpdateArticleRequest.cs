namespace Application.Dtos.Article.Requests;
public record UpdateArticleRequest(
    string Title,
    string Image,
    string Description,
    string DescriptionHtml,
    string PublishedAt,
    List<string> TagIds);
