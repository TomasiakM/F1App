using Application.Interfaces;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons.ValueObjects;
using Domain.Aggregates.Tracks.ValueObjects;
using MediatR;

namespace Application.Features.RaceWeeks.Commands.Create;
internal sealed class CreateRaceWeekCommandHandler : IRequestHandler<CreateRaceWeekCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRaceWeekCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateRaceWeekCommand request, CancellationToken cancellationToken)
    {
        var raceWeek = RaceWeek.Create(
            request.Name,
            TrackId.Create(new(request.TrackId)),
            SeasonId.Create(new(request.SeasonId)));

        CreateSessions(request, raceWeek);

        _unitOfWork.RaceWeeks.Add(raceWeek);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private static void CreateSessions(CreateRaceWeekCommand request, RaceWeek raceWeek)
    {
        if (request.FP1 is not null)
        {
            raceWeek.CreateFp1Session(DateTimeOffset.Parse(request.FP1));
        }

        if (request.FP2 is not null)
        {
            raceWeek.CreateFp2Session(DateTimeOffset.Parse(request.FP2));
        }

        if (request.FP3 is not null)
        {
            raceWeek.CreateFp3Session(DateTimeOffset.Parse(request.FP3));
        }

        if (request.SprintQualification is not null)
        {
            raceWeek.CreateSprintQualificationSession(DateTimeOffset.Parse(request.SprintQualification));
        }

        if (request.Sprint is not null)
        {
            raceWeek.CreateSprintSession(DateTimeOffset.Parse(request.Sprint));
        }

        if (request.RaceQualification is not null)
        {
            raceWeek.CreateRaceQualificationSession(DateTimeOffset.Parse(request.RaceQualification));
        }

        if (request.Race is not null)
        {
            raceWeek.CreateRaceSession(DateTimeOffset.Parse(request.Race));
        }
    }
}
