namespace Application.Dtos.Track.Requests;
public record UpdateTrackRequest(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml,
    int Length,
    int Corners,
    string City);