using Application.Interfaces;
using Domain.Aggregates.RaceWeeks.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.Update;
internal sealed class UpdateRaceWeekCommandHandler : IRequestHandler<UpdateRaceWeekCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRaceWeekCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateRaceWeekCommand request, CancellationToken cancellationToken)
    {
        var raceWeekId = RaceWeekId.Create(request.RaceWeekId);
        var raceWeek = await _unitOfWork.RaceWeeks.GetAsync(raceWeekId);

        if(raceWeek is null)
        {
            throw new NotFoundException();
        }

        raceWeek.Update(
            request.Name, 
            TrackId.Create(new(request.TrackId)));

        if (request.FP1 is not null)
        {
            raceWeek.UpdateFp1Session(DateTime.Parse(request.FP1));
        }

        if (request.FP2 is not null)
        {
            raceWeek.UpdateFp2Session(DateTime.Parse(request.FP2));
        }

        if (request.FP3 is not null)
        {
            raceWeek.UpdateFp3Session(DateTime.Parse(request.FP3));
        }

        if (request.SprintQualification is not null)
        {
            raceWeek.UpdateSprintQualificationSession(DateTime.Parse(request.SprintQualification));
        }

        if (request.Sprint is not null)
        {
            raceWeek.UpdateSprintSession(DateTime.Parse(request.Sprint));
        }

        if (request.RaceQualification is not null)
        {
            raceWeek.UpdateRaceQualificationSession(DateTime.Parse(request.RaceQualification));
        }

        if (request.Race is not null)
        {
            raceWeek.UpdateRaceSession(DateTime.Parse(request.Race));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
