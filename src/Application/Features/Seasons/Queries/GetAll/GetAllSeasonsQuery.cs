using Application.Dtos.Season.Responses;
using MediatR;

namespace Application.Features.Seasons.Queries.GetAll;
public record GetAllSeasonsQuery() : IRequest<List<SeasonResponse>>;
