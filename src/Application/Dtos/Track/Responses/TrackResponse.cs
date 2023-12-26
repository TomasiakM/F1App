namespace Application.Dtos.Track.Responses;
public record TrackResponse(
    Guid Id,
    string Name,
    string Slug,
    string Image,
    string CountryCode,
    string DescriptionHtml,
    int Length,
    int Corners,
    string City);
