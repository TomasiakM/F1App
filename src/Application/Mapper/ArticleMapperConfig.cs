using Application.Dtos.Article.Requests;
using Application.Dtos.Article.Responses;
using Application.Features.Articles.Commands.Create;
using Domain.Aggregates.Articles;
using Domain.Aggregates.Tags;
using Mapster;

namespace Application.Mapper;
internal sealed class ArticleMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateArticleRequest, CreateArticleCommand>();

        config.NewConfig<Article, ArticleResponse>()
            .Map(dest => dest.Likes, src => src.Likes.Count())
            .Map(dest => dest.Id, src => src.Id.Value);

        config.NewConfig<(Article article, List<Tag> tags), ArticleResponse>()
            .Map(dest => dest.Likes, src => src.article.Likes.Count())
            .Map(dest => dest.Id, src => src.article.Id.Value)
            .Map(dest => dest.Tags, src => src.tags)
            .Map(dest => dest, src => src.article);
    }
}
