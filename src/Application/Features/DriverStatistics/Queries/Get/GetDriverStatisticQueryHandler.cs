using Application.Dtos.DriverStatistic.Responses;
using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.DriverStatistics.Queries.Get;
internal sealed class GetDriverStatisticQueryHandler : IRequestHandler<GetDriverStatisticQuery, DriverStatisticResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverStatisticQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DriverStatisticResponse> Handle(GetDriverStatisticQuery request, CancellationToken cancellationToken)
    {
        var driverId = DriverId.Create(request.DriverId);
        var driverStatistic = await _unitOfWork.DriverStatistics.FindAsync(e => e.DriverId == driverId);

        if (driverStatistic is null)
        {
            throw new NotFoundException();
        }

        var driverStatisticDto = _mapper.Map<DriverStatisticResponse>(driverStatistic);

        return driverStatisticDto;
    }
}
