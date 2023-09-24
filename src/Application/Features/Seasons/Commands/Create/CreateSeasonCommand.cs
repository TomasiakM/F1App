using MediatR;

namespace Application.Features.Seasons.Commands.Create;
public record CreateSeasonCommand(
    int Year): IRequest<Unit>;
