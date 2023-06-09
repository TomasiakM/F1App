using Application.Dtos.Tag.Responses;
using Application.Interfaces;
using Domain.Aggregates.Tags;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tags.Commands.Create;
internal sealed class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateTagCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TagResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = Tag.Create(request.Name);

        _unitOfWork.Tags.Add(tag);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var tagDto = _mapper.Map<TagResponse>(tag);
        return tagDto;
    }
}
