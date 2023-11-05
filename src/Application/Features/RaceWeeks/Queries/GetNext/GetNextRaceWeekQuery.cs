using Application.Dtos.RaceWeek.Responses;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.GetNext;
public record GetNextRaceWeekQuery : IRequest<RaceWeekResponse>;
