using Application.Dtos.Article.Responses;
using Application.Interfaces;
using Domain.Aggregates.Articles;
using Domain.Aggregates.Tags.ValueObjects;
using Domain.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Articles.Commands.Create;
internal sealed class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDateProvider _dateTimeProvider;

    public CreateArticleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IDateProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var tagIds = request.TagIds.Select(e => TagId.Create(new Guid(e))).ToList();
        
        var article = Article.Create(
            request.Title,
            request.Image,
            request.Description,
            request.DescriptionHtml,
            DateTimeOffset.Parse(request.PublishedAt),
            tagIds,
            _dateTimeProvider);

        _unitOfWork.Articles.Add(article);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var articleDto = _mapper.Map<ArticleResponse>(article);

        return articleDto;
    }
}
