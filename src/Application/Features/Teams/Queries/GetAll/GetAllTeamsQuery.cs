using Application.Dtos.Team.Responses;
using MediatR;

namespace Application.Features.Teams.Queries.GetAll;
public record GetAllTeamsQuery() : IRequest<List<TeamResponse>>;
