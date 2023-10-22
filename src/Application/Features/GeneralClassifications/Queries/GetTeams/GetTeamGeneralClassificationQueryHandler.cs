using Application.Dtos.GeneralClassification.Responses;
using Application.Dtos.Team.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.GeneralClassifications.Queries.GetTeams;
internal sealed class GetTeamGeneralClassificationQueryHandler : IRequestHandler<GetTeamGeneralClassificationQuery, List<TeamGeneralClassificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamGeneralClassificationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TeamGeneralClassificationResponse>> Handle(GetTeamGeneralClassificationQuery request, CancellationToken cancellationToken)
    {
        var season = await _unitOfWork.Seasons.FindAsync(e => e.Year == request.Year);
        if (season is null)
        {
            throw new NotFoundException();
        }

        var generalClassification = await _unitOfWork.GeneralClassifications.FindAsync(e => e.SeasonId == season.Id);
        if (generalClassification is null)
        {
            throw new NotFoundException();
        }

        var teamIds = generalClassification.Teams.Select(e => e.TeamId).ToList();
        var teams = await _unitOfWork.Teams.FindAllAsync(e => teamIds.Contains(e.Id));

        var teamGeneralClassification = generalClassification.Teams.Select(e =>
            new TeamGeneralClassificationResponse(
                e.Place,
                e.Points,
                _mapper.Map<TeamResponse>(teams.First(team => team.Id == e.TeamId))))
            .ToList();

        return teamGeneralClassification;
    }
}
