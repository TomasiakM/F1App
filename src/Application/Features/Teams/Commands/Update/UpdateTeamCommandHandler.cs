using Application.Interfaces;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Teams.Commands.Update;
internal sealed class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.Create(request.TeamId);
        var team = await _unitOfWork.Teams.GetAsync(teamId);

        if(team is null)
        {
            throw new NotFoundException();
        }

        team.Update(
            request.Name,
            request.Image,
            request.CountryCode,
            request.DescriptionHtml);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
