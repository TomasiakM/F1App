using Application.Dtos.Driver.Responses;
using Application.Dtos.Team.Responses;

namespace Application.Dtos.RaceWeek.Responses.Session;
public record QualificationResultResponse(
    int Place,
    TimeSpan? Q1Time,
    TimeSpan? Q2Time,
    TimeSpan? Q3Time,
    DriverResponse Driver,
    TeamResponse Team);
