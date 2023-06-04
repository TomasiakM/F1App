namespace Application.Dtos.Auth.Requests;
public record LoginRequest(
    string Username,
    string Password);
