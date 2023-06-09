namespace Application.Dtos.Article.Requests;
public record CreateArticleRequest(
    string Title,
    string Image,
    string Description,
    string DescriptionHtml,
    string PublishedAt,
    List<string> TagIds);
