using Application.Interfaces;
using Domain.Aggregates.Comments.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Comments.Commands.AddReply;
internal sealed class AddReplyCommandHandler : IRequestHandler<AddReplyCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;
    private readonly IUserService _userService;

    public AddReplyCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _userService = userService;
    }

    public async Task<Unit> Handle(AddReplyCommand request, CancellationToken cancellationToken)
    {
        var commentId = CommentId.Create(request.CommentId);
        var comment = await _unitOfWork.Comments.GetAsync(commentId);

        if (comment is null)
        {
            throw new NotFoundException();
        }

        comment.AddReply(
            _userService.GetUserId(),
            request.Text,
            _dateProvider);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
