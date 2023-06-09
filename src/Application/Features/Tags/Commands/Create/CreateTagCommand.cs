using Application.Dtos.Tag.Responses;
using MediatR;

namespace Application.Features.Tags.Commands.Create;
public record CreateTagCommand(
    string Name) : IRequest<TagResponse>;
