using Application.Interfaces;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Teams.Commands.Delete;
internal sealed class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = TeamId.Create(request.TeamId);
        var team = await _unitOfWork.Teams.GetAsync(teamId);

        if(team is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Teams.Delete(team);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
