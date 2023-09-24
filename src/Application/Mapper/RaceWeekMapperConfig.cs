using Application.Dtos.RaceWeek.Requests;
using Application.Dtos.RaceWeek.Responses;
using Application.Features.RaceWeeks.Commands.Create;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Tracks;
using Mapster;

namespace Application.Mapper;
internal sealed class RaceWeekMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRaceWeekRequest, CreateRaceWeekCommand>();

        config.NewConfig<(RaceWeek raceWeek, Season season, Track track), RaceWeekResponse>()
            .Map(dest => dest.Id, src => src.raceWeek.Id.Value)
            .Map(dest => dest.FP1, src => src.raceWeek.FP1!.Start)
            .Map(dest => dest.FP2, src => src.raceWeek.FP2!.Start)
            .Map(dest => dest.FP3, src => src.raceWeek.FP3!.Start)
            .Map(dest => dest.SprintQualification, src => src.raceWeek.SprintQualifications!.Start)
            .Map(dest => dest.Sprint, src => src.raceWeek.Sprint!.Start)
            .Map(dest => dest.RaceQualification, src => src.raceWeek.RaceQualifications!.Start)
            .Map(dest => dest.Race, src => src.raceWeek.Race!.Start)
            .Map(dest => dest.Season, src => src.season)
            .Map(dest => dest.Track, src => src.track)
            .Map(dest => dest, src => src.raceWeek);
    }
}
