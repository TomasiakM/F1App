using Application.Dtos.Common;
using Application.Dtos.Tag.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tags.Queries.GetPaginated;
internal sealed class GetPaginatedTagsQueryHandler : IRequestHandler<GetPaginatedTagsQuery, Pagination<TagResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedTagsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TagResponse>> Handle(GetPaginatedTagsQuery request, CancellationToken cancellationToken)
    {
        var (tags, pages) = await _unitOfWork.Tags.GetPaginatedAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var tagDtos = _mapper.Map<List<TagResponse>>(tags);

        return new(request.Filters.Page, request.Filters.PageSize, pages, tagDtos);
    }
}
