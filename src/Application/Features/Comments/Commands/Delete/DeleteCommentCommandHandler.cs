using Application.Interfaces;
using Domain.Aggregates.Comments.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Comments.Commands.Delete;
internal sealed class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var commentId = CommentId.Create(request.CommentId);
        var comment = await _unitOfWork.Comments.GetAsync(commentId);

        if (comment is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Comments.Delete(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
