using Application.Dtos.Driver.Responses;
using Application.Interfaces;
using MapsterMapper;
using MediatR;

namespace Application.Features.Drivers.Queries.GetAll;
internal sealed class GetAllDriverQueryHandler : IRequestHandler<GetAllDriversQuery, List<DriverResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<DriverResponse>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var drivers = await _unitOfWork.Drivers.GetAllAsync(cancellationToken);
        var driverDtos = _mapper.Map<List<DriverResponse>>(drivers);

        return driverDtos;
    }
}
