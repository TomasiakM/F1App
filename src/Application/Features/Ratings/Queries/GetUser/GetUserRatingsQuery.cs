using Application.Dtos.Rating.Responses;
using MediatR;

namespace Application.Features.Ratings.Queries.GetUser;
public record GetUserRatingsQuery(
    Guid RatingId) : IRequest<UserDriverRatingsResponse>;
