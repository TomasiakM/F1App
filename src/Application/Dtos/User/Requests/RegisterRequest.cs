namespace Application.Dtos.Auth.Requests;
public record RegisterRequest(
    string Username,
    string Password,
    string Email);
