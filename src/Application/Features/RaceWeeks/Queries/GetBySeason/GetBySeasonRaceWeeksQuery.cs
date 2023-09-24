using Application.Dtos.RaceWeek.Responses;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetBySeason;
public record GetBySeasonRaceWeeksQuery(
    Guid SeasonId): IRequest<List<RaceWeekResponse>>;
