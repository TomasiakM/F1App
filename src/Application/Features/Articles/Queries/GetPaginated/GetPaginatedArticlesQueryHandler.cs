using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginated;
internal sealed class GetPaginatedArticlesQueryHandler : IRequestHandler<GetPaginatedArticlesQuery, Pagination<ArticleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedArticlesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<ArticleResponse>> Handle(GetPaginatedArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _unitOfWork.Articles.GetPagginatedAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);
        var pages = await _unitOfWork.Articles.GetPageCountAsync(request.Filters.PageSize, cancellationToken);
        
        var articleDtos = _mapper.Map<List<ArticleResponse>>(articles);

        return new Pagination<ArticleResponse>(
            request.Filters.Page, 
            request.Filters.PageSize,
            pages,
            articleDtos);
    }
}
