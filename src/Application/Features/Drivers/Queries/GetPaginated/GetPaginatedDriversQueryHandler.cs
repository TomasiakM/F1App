using Application.Dtos.Common;
using Application.Dtos.Driver.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Drivers.Queries.GetPaginated;
internal sealed class GetPaginatedDriversQueryHandler : IRequestHandler<GetPaginatedDriversQuery, Pagination<DriverResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedDriversQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Pagination<DriverResponse>> Handle(GetPaginatedDriversQuery request, CancellationToken cancellationToken)
    {
        var (drivers, pages) = await _unitOfWork.Drivers.GetPaginatedAsync(request.Filters.Page, request.Filters.PageSize, cancellationToken);

        var driverDtos = _mapper.Map<List<DriverResponse>>(drivers);

        return new(
            request.Filters.Page,
            request.Filters.PageSize,
            pages,
            driverDtos);
    }
}
