using MediatR;

namespace Application.Features.Tags.Commands.Delete;
public record DeleteTagCommand(
    Guid TagId) : IRequest<Unit>;
