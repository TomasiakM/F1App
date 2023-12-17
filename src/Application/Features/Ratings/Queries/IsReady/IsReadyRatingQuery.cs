using Application.Dtos.Rating.Responses;
using MediatR;

namespace Application.Features.Ratings.Queries.IsReadyToStart;
public record IsReadyRatingQuery(
    Guid RaceWeekId) : IRequest<RatingIsReadyResponse>;
