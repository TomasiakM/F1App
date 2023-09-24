using Application.Dtos.Driver.Responses;
using Application.Interfaces;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.Drivers.Queries.Get;
internal sealed class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDriverQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<DriverResponse> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = await _unitOfWork.Drivers.FindAsync(e => e.Slug == request.Slug);

        if(driver is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<DriverResponse>(driver);
    }
}
