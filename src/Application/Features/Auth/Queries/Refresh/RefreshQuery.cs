using Application.Dtos.Auth.Responses;
using MediatR;

namespace Application.Features.Auth.Queries.Refresh;
public record RefreshQuery() : IRequest<AuthResponse>;
