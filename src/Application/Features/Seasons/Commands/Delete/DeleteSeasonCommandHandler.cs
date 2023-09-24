using Application.Interfaces;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Seasons.Commands.Delete;
internal sealed class DeleteSeasonCommandHandler : IRequestHandler<DeleteSeasonCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSeasonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteSeasonCommand request, CancellationToken cancellationToken)
    {
        var seasonId = SeasonId.Create(request.SeasonId);
        var season = await _unitOfWork.Seasons.GetAsync(seasonId);

        if (season is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Seasons.Delete(season);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
