using Application.Dtos.Season.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Seasons.Queries.GetAll;
internal sealed class GetAllSeasonsQueryHandler : IRequestHandler<GetAllSeasonsQuery, List<SeasonResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllSeasonsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SeasonResponse>> Handle(GetAllSeasonsQuery request, CancellationToken cancellationToken)
    {
        var seasons = await _unitOfWork.Seasons.GetAllAsync(cancellationToken);
        var seasonsDto = _mapper.Map<List<SeasonResponse>>(seasons);

        return seasonsDto;
    }
}
