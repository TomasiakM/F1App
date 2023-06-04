namespace Application.Dtos.User.Requests;
public record BanUserRequest(
    int BanDays,
    string Reason);
