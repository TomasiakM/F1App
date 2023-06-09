using Application.Dtos.Tag.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Tags.Queries.GetAll;
internal sealed class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllTagsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TagResponse>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _unitOfWork.Tags.GetAllAsync(cancellationToken);

        var tagDtos = _mapper.Map<List<TagResponse>>(tags);
        return tagDtos;
    }
}
