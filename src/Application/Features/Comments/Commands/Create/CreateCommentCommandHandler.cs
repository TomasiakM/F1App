using Application.Interfaces;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Comments;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Comments.Commands.Create;
internal sealed class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;
    private readonly IUserService _userService;

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
        _userService = userService;
    }

    public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = Comment.Create(
            ArticleId.Create(request.ArticleId),
            _userService.GetUserId(),
            request.Text,
            _dateProvider);

        _unitOfWork.Comments.Add(comment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
