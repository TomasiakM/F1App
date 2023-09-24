using Application.Dtos.Team.Responses;
using MediatR;

namespace Application.Features.Teams.Queries.Get;
public record GetTeamQuery(
    string Slug) : IRequest<TeamResponse>;
