using Application.Dtos.Driver.Responses;
using Application.Dtos.GeneralClassification.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.GeneralClassifications.Queries.GetDrivers;
internal sealed class GetDriverGeneralClassificationQueryHandler : IRequestHandler<GetDriverGeneralClassificationQuery, List<DriverGeneralClassificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverGeneralClassificationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<DriverGeneralClassificationResponse>> Handle(GetDriverGeneralClassificationQuery request, CancellationToken cancellationToken)
    {
        var season = await _unitOfWork.Seasons.FindAsync(e => e.Year == request.Year);
        if(season is null)
        {
            throw new NotFoundException();
        }

        var generalClassification = await _unitOfWork.GeneralClassifications.FindAsync(e => e.SeasonId == season.Id);
        if(generalClassification is null)
        {
            throw new NotFoundException();
        }

        var driverIds = generalClassification.Drivers.Select(e => e.DriverId).ToList();
        var drivers = await _unitOfWork.Drivers.FindAllAsync(e => driverIds.Contains(e.Id));

        var driverGeneralClassification = generalClassification.Drivers.Select(e => 
            new DriverGeneralClassificationResponse(
                e.Place, 
                e.Points, 
                _mapper.Map<DriverResponse>(drivers.First(driver => driver.Id == e.DriverId))))
            .ToList();

        return driverGeneralClassification;
    }
}
