using Application.Interfaces;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Tags.Commands.Delete;
internal sealed class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tagId = TagId.Create(request.TagId);
        var tag = await _unitOfWork.Tags.GetAsync(tagId);

        if (tag is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Tags.Delete(tag);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
