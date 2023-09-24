using Application.Dtos.Common;
using Application.Dtos.Team.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Teams.Queries.GetPaginated;
internal sealed class GetPaginatedTeamsQueryHandler : IRequestHandler<GetPaginatedTeamsQuery, Pagination<TeamResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedTeamsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<TeamResponse>> Handle(GetPaginatedTeamsQuery request, CancellationToken cancellationToken)
    {
        var (teams, totalPages) = await _unitOfWork.Teams.GetPaginatedAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var teamDtos = _mapper.Map<List<TeamResponse>>(teams);

        return new(
            request.Filters.Page,
            request.Filters.PageSize,
            totalPages,
            teamDtos);
    }
}
