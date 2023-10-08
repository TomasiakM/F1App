using Application.Dtos.Driver.Responses;
using Application.Dtos.Team.Responses;

namespace Application.Dtos.RaceWeek.Responses.Session;
public record FreePracticeResultResponse(
    int Place,
    int Laps,
    TimeSpan? FastestLap,
    DriverResponse Driver,
    TeamResponse Team);
