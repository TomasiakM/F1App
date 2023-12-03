using MediatR;

namespace Application.Features.Ratings.Commands.Summarize;
public record SummarizeRatingCommand(
    Guid RatingId) : IRequest<Unit>;
