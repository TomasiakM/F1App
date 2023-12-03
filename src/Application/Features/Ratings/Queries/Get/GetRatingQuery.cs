using Application.Dtos.Rating.Responses;
using MediatR;

namespace Application.Features.Ratings.Queries.Get;
public record GetRatingQuery(
    Guid RaceWeekId) : IRequest<RatingSummaryResponse>;
