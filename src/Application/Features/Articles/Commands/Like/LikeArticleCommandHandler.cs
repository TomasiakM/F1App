using Application.Interfaces;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Articles.Commands.Like;
internal sealed class LikeArticleCommandHandler : IRequestHandler<LikeArticleCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IDateProvider _dateProvider;

    public LikeArticleCommandHandler(IUnitOfWork unitOfWork, IUserService userService, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(LikeArticleCommand request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.Create(request.ArticleId);
        var article = await _unitOfWork.Articles.GetAsync(articleId);

        if(article is null)
        {
            throw new NotFoundException();
        }

        article.AddLike(
            _userService.GetUserId(),
            _dateProvider);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
