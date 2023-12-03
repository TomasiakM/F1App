using Application.Dtos.Rating.Requests;
using Application.Features.Ratings.Commands.AddRatings;
using Mapster;

namespace Application.Mapper;
internal sealed class RatingMapperConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid ratingId, AddRatingsRequest request), AddRatingsCommand>()
            .Map(dest => dest.RatingId, src => src.ratingId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<DriverRatingRequest, DriverRatingCommand>();
    }
}
