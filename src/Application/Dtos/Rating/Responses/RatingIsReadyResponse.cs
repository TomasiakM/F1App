namespace Application.Dtos.Rating.Responses;
public record RatingIsReadyResponse(
    bool IsCreated,
    bool IsReadyToCreate,
    bool IsSummarized,
    bool IsReadyToSummerize,
    Guid? RatingId);
