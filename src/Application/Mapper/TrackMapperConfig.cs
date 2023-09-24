using Application.Dtos.Track.Requests;
using Application.Dtos.Track.Responses;
using Application.Features.Tracks.Commands.Create;
using Application.Features.Tracks.Commands.Update;
using Domain.Aggregates.Tracks;
using Mapster;

namespace Application.Mapper;
internal sealed class TrackMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTrackRequest, CreateTrackCommand>();

        config.NewConfig<(Guid trackId, UpdateTrackRequest request), UpdateTrackCommand>()
            .Map(dest => dest.TrackId, src => src.trackId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Track, TrackResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
