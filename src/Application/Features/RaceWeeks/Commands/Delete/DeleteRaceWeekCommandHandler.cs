using Application.Interfaces;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.Delete;
internal sealed class DeleteRaceWeekCommandHandler : IRequestHandler<DeleteRaceWeekCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRaceWeekCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteRaceWeekCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);
        
        if (raceWeek is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.RaceWeeks.Delete(raceWeek);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
