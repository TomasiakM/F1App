using Application.Interfaces;
using Domain.Aggregates.Teams;
using MediatR;

namespace Application.Features.Teams.Commands.Create;
internal sealed class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeamCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = Team.Create(
            request.Name,
            request.Image,
            request.CountryCode,
            request.DescriptionHtml);

        _unitOfWork.Teams.Add(team);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
