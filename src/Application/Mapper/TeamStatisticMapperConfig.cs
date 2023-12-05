using Application.Dtos.TeamStatistic.Responses;
using Domain.Aggregates.TeamStatistics;
using Mapster;

namespace Application.Mapper;
internal sealed class TeamStatisticMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TeamStatistic, TeamStatisticResponse>()
            .Map(dest => dest.TeamId, src => src.TeamId.Value);
    }
}
