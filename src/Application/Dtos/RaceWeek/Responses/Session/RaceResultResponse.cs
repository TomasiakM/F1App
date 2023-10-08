using Application.Dtos.Driver.Responses;
using Application.Dtos.Team.Responses;

namespace Application.Dtos.RaceWeek.Responses.Session;
public record RaceResultResponse(
    int Place,
    int Laps,
    TimeSpan? FastestLap,
    string FinishType,
    DriverResponse Driver,
    TeamResponse Team,
    float Points,
    TimeSpan? FinishTime,
    int StartPosition);
