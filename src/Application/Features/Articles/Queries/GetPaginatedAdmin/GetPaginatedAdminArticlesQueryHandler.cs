using Application.Dtos.Article.Responses;
using Application.Dtos.Common;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Articles.Queries.GetPaginatedAdmin;
internal sealed class GetPaginatedAdminArticlesQueryHandler : IRequestHandler<GetPaginatedAdminArticlesQuery, Pagination<ArticleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedAdminArticlesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<ArticleResponse>> Handle(GetPaginatedAdminArticlesQuery request, CancellationToken cancellationToken)
    {
        var (articles, pages) = await _unitOfWork.Articles.GetPaginatedAdminAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var articleDtos = _mapper.Map<List<ArticleResponse>>(articles);

        return new(request.Filters.Page, request.Filters.PageSize, pages, articleDtos);
    }
}
