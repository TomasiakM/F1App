using Application.Interfaces;
using Domain.Aggregates.Comments.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Comments.Commands.DeleteReply;
internal sealed class DeleteReplyCommandHandler : IRequestHandler<DeleteReplyCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReplyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteReplyCommand request, CancellationToken cancellationToken)
    {
        var commentId = CommentId.Create(request.CommentId);
        var comment = await _unitOfWork.Comments.GetAsync(commentId);

        if (comment is null)
        {
            throw new NotFoundException();
        }

        var replyId = ReplyId.Create(request.ReplyId);
        comment.DeleteReply(replyId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
