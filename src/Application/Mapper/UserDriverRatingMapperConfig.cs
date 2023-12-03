using Application.Dtos.Rating.Responses;
using Domain.Aggregates.UserDriverRatings;
using Mapster;

namespace Application.Mapper;
internal sealed class UserDriverRatingMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDriverRating, UserDriverRatingResponse>()
            .Map(dest => dest.DriverId, src => src.DriverId.Value);
    }
}
