using Application.Dtos.Driver.Requests;
using Application.Dtos.Driver.Responses;
using Application.Features.Drivers.Commands.Create;
using Application.Features.Drivers.Commands.Update;
using Domain.Aggregates.Drivers;
using Mapster;

namespace Application.Mapper;
internal sealed class DriverMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateDriverRequest, CreateDriverCommand>();

        config.NewConfig<(Guid driverId, UpdateDriverRequest request), UpdateDriverCommand>()
            .Map(dest => dest.DriverId, src => src.driverId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Driver, DriverResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
