using Application.Dtos.RaceWeek.Requests;
using Application.Dtos.RaceWeek.Responses;
using Application.Dtos.RaceWeek.Responses.Session;
using Application.Features.RaceWeeks.Commands.Create;
using Application.Features.RaceWeeks.Commands.Update;
using Application.Features.RaceWeeks.Commands.UpdateFP1SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateFP2SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateFP3SessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateRaceSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateSprintQualificationSessionResults;
using Application.Features.RaceWeeks.Commands.UpdateSprintSessionResults;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.RaceWeeks.ValueObjects.SessionResults;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Teams;
using Domain.Aggregates.Tracks;
using Mapster;

namespace Application.Mapper;
internal sealed class RaceWeekMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateRaceWeekRequest, CreateRaceWeekCommand>();

        config.NewConfig<(Guid raceWeekId, UpdateRaceWeekRequest request), UpdateRaceWeekCommand>()
            .Map(dest => dest.RaceWeekId, src => src.raceWeekId)
            .Map(dest => dest, src => src.request);


        // RaceWeek Session to SessionResponse
        config.NewConfig<(FP1Result fp1, Driver driver, Team team), FreePracticeResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.fp1);

        config.NewConfig<(FP2Result fp2, Driver driver, Team team), FreePracticeResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.fp2);

        config.NewConfig<(FP3Result fp3, Driver driver, Team team), FreePracticeResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.fp3);

        config.NewConfig<(SprintQualificationResult sprintQuali, Driver driver, Team team), QualificationResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.sprintQuali);

        config.NewConfig<(SprintResult sprint, Driver driver, Team team), RaceResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.sprint);

        config.NewConfig<(RaceQualificationResult raceQuali, Driver driver, Team team), QualificationResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.raceQuali);

        config.NewConfig<(RaceResult race, Driver driver, Team team), RaceResultResponse>()
            .Map(dest => dest.Driver, src => src.driver)
            .Map(dest => dest.Team, src => src.team)
            .Map(dest => dest, src => src.race);

        // RaceWeek to RaceWeekResponse
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

        // FreePractices mapping
        config.NewConfig<UpdateFreePracticeSessionResultRequest, UpdateFP1SessionResultCommand>();
        config.NewConfig<UpdateFreePracticeSessionResultRequest, UpdateFP2SessionResultCommand>();
        config.NewConfig<UpdateFreePracticeSessionResultRequest, UpdateFP3SessionResultCommand>();

        config.NewConfig<(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request), UpdateFP1SessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        config.NewConfig<(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request), UpdateFP2SessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        config.NewConfig<(Guid raceWeekId, UpdateFreePracticeSessionResultsRequest request), UpdateFP3SessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        // Qualifications mapping
        config.NewConfig<UpdateQualificationSessionResultRequest, UpdateSprintQualificationSessionResultCommand>();
        config.NewConfig<UpdateQualificationSessionResultRequest, UpdateRaceQualificationSessionResultCommand>();

        config.NewConfig<(Guid raceWeekId, UpdateQualificationSessionResultsRequest request), UpdateSprintQualificationSessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        config.NewConfig<(Guid raceWeekId, UpdateQualificationSessionResultsRequest request), UpdateRaceQualificationSessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        // Race mapping
        config.NewConfig<UpdateRaceSessionResultRequest, UpdateSprintSessionResultCommand>();
        config.NewConfig<UpdateRaceSessionResultRequest, UpdateRaceSessionResultCommand>();

        config.NewConfig<(Guid raceWeekId, UpdateRaceSessionResultsRequest request), UpdateSprintSessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);

        config.NewConfig<(Guid raceWeekId, UpdateRaceSessionResultsRequest request), UpdateRaceSessionResultsCommand>()
            .Map(e => e.RaceWeekId, src => src.raceWeekId)
            .Map(e => e.SessionResults, src => src.request.SessionResults);
    }
}
