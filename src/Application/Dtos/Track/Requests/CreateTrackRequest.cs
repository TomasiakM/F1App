namespace Application.Dtos.Track.Requests;
public record CreateTrackRequest(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml,
    int Length,
    int Corners,
    string City);
