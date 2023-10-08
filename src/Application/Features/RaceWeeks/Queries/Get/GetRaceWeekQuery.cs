using Application.Dtos.RaceWeek.Responses;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.Get;
public record GetRaceWeekQuery(
    int Year, 
    string Slug) : IRequest<RaceWeekDetailResponse>;
