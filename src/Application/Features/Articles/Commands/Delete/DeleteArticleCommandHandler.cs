using Application.Interfaces;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Articles.Commands.Delete;
internal sealed class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteArticleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.Create(request.ArticleId);
        var article = await _unitOfWork.Articles.GetAsync(articleId, cancellationToken);

        if(article is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Articles.Delete(article);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
