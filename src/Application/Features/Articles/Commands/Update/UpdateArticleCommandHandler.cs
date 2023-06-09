using Application.Interfaces;
using Domain.Aggregates.Articles.ValueObjects;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Articles.Commands.Update;
internal sealed class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateProvider _dateProvider;

    public UpdateArticleCommandHandler(IUnitOfWork unitOfWork, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var articleId = ArticleId.Create(request.ArticleId);
        var article = await _unitOfWork.Articles.GetAsync(articleId);

        if(article is null)
        {
            throw new NotFoundException();
        }

        var tagIds = request.TagIds.Select(e => TagId.Create(new Guid(e))).ToList();
        article.Update(
            request.Title,
            request.Image,
            request.Description,
            request.DescriptionHtml,
            DateTimeOffset.Parse(request.PublishedAt),
            tagIds,
            _dateProvider);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
