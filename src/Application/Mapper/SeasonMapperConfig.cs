using Application.Dtos.Season.Requests;
using Application.Dtos.Season.Responses;
using Application.Features.Seasons.Commands.Create;
using Domain.Aggregates.Seasons;
using Mapster;

namespace Application.Mapper;
internal sealed class SeasonMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateSeasonRequest, CreateSeasonCommand>();

        config.NewConfig<Season, SeasonResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
