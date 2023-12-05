using Application.Dtos.TeamStatistic.Responses;
using MediatR;

namespace Application.Features.TeamStatistics.Queries.Get;
public record GetTeamStatisticQuery(
    Guid TeamId) : IRequest<TeamStatisticResponse>;
