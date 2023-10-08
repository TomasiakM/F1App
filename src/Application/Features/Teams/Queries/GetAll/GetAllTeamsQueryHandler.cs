using Application.Dtos.Team.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Teams.Queries.GetAll;
internal sealed class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, List<TeamResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTeamsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TeamResponse>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _unitOfWork.Teams.GetAllAsync(cancellationToken);
        var teamDtos = _mapper.Map<List<TeamResponse>>(teams);

        return teamDtos;
    }
}
