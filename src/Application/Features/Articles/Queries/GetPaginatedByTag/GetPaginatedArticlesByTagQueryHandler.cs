using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginatedByTag;
internal sealed class GetPaginatedArticlesByTagQueryHandler : IRequestHandler<GetPaginatedArticlesByTagQuery, Pagination<ArticleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedArticlesByTagQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<ArticleResponse>> Handle(GetPaginatedArticlesByTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.FindAsync(e => e.Slug == request.TagSlug);

        if (tag is null)
        {
            return new(1, 0, 1, new());
        }

        var (articles, pages) = await _unitOfWork.Articles.GetPaginatedByTagIdAsync(tag.Id, request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var articleDtos = _mapper.Map<List<ArticleResponse>>(articles);

        return new Pagination<ArticleResponse>(
            request.Filters.Page,
            request.Filters.PageSize,
            pages,
            articleDtos);
    }
}
