using Application.Dtos.Article.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.Articles.Queries.GetArticle;
internal sealed class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetArticleQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ArticleResponse> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var article = await _unitOfWork.Articles.FindAsync(e => e.Slug == request.Slug);
        if(article is null)
        {
            throw new NotFoundException();
        }

        var tags = await _unitOfWork.Tags.FindAllAsync(e => article.TagIds.Contains(e.Id));

        var articleDto = _mapper.Map<ArticleResponse>((article, tags.ToList()));
        return articleDto;
    }
}
