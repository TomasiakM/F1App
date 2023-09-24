namespace Application.Dtos.Track.Requests;
public record UpdateTrackRequest(
    string Name,
    string Image,
    string CountryCode,
    string DescriptionHtml);