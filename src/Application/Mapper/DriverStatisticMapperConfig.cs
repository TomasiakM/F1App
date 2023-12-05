using Application.Dtos.DriverStatistic.Responses;
using Domain.Aggregates.DriverStatistics;
using Mapster;

namespace Application.Mapper;
internal sealed class DriverStatisticMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DriverStatistic, DriverStatisticResponse>()
            .Map(dest => dest.DriverId, src => src.DriverId.Value);
    }
}
