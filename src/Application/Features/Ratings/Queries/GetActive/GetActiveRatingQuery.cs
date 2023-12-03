using Application.Dtos.Rating.Responses;
using MediatR;

namespace Application.Features.Ratings.Queries.GetActive;
public record GetActiveRatingQuery() : IRequest<RatingResponse>;