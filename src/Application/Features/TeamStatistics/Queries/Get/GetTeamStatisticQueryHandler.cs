using Application.Dtos.TeamStatistic.Responses;
using Application.Interfaces;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.TeamStatistics.Queries.Get;
internal sealed class GetTeamStatisticQueryHandler : IRequestHandler<GetTeamStatisticQuery, TeamStatisticResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamStatisticQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamStatisticResponse> Handle(GetTeamStatisticQuery request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.Create(request.TeamId);
        var teamStatistic = await _unitOfWork.TeamStatistics.FindAsync(e => e.TeamId == teamId, cancellationToken);

        if(teamStatistic is null)
        {
            throw new NotFoundException();
        }

        var teamStatisticDto = _mapper.Map<TeamStatisticResponse>(teamStatistic);

        return teamStatisticDto;
    }
}
