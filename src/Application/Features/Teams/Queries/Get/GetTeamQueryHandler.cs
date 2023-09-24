using Application.Dtos.Team.Responses;
using Application.Interfaces;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.Teams.Queries.Get;
internal sealed class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, TeamResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTeamQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TeamResponse> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.Teams.FindAsync(e => e.Slug == request.Slug);

        if (team is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<TeamResponse>(team);
    }
}
