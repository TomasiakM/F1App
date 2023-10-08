using Application.Dtos.RaceWeek.Responses;
using Application.Dtos.RaceWeek.Responses.Session;
using Application.Dtos.Season.Responses;
using Application.Dtos.Track.Responses;
using Application.Interfaces;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Teams;
using Domain.Aggregates.Teams.ValueObjects;
using Domain.Aggregates.Tracks;
using Domain.Exceptions;
using MapsterMapper;
using MediatR;

namespace Application.Features.RaceWeeks.Queries.Get;
internal sealed class GetRaceWeekQueryHandler : IRequestHandler<GetRaceWeekQuery, RaceWeekDetailResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetRaceWeekQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RaceWeekDetailResponse> Handle(GetRaceWeekQuery request, CancellationToken cancellationToken)
    {
        var season = await _unitOfWork.Seasons.FindAsync(e => e.Year == request.Year);

        if(season is null)
        {
            throw new NotFoundException();
        }

        var raceWeek = await _unitOfWork.RaceWeeks.FindAsync(e => e.SeasonId == season.Id && e.Slug == request.Slug);

        if (raceWeek is null)
        {
            throw new NotFoundException();
        }


        var teamIds = GetAllTeamIds(raceWeek);
        var driverIds = GetAllDriverIds(raceWeek);

        var teams = await _unitOfWork.Teams.FindAllAsync(e => teamIds.Contains(e.Id));
        var drivers = await _unitOfWork.Drivers.FindAllAsync(e => driverIds.Contains(e.Id));
        var track = await _unitOfWork.Tracks.GetAsync(raceWeek.TrackId);

        return MapResponse(season, track!, raceWeek, teams, drivers);
    }

    private RaceWeekDetailResponse MapResponse(Season season, Track track, RaceWeek raceWeek, ICollection<Team> teams, ICollection<Driver> drivers)
    {
        SessionResponse<FreePracticeResultResponse>? fp1 = null;
        if(raceWeek.FP1 is not null)
        {
            fp1 = new SessionResponse<FreePracticeResultResponse>(
                raceWeek.FP1.Start,
                raceWeek.FP1.SessionResults.Select(sr =>
                    _mapper.Map<FreePracticeResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<FreePracticeResultResponse>? fp2 = null;
        if (raceWeek.FP2 is not null)
        {
            fp2 = new SessionResponse<FreePracticeResultResponse>(
                raceWeek.FP2.Start,
                raceWeek.FP2.SessionResults.Select(sr =>
                    _mapper.Map<FreePracticeResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<FreePracticeResultResponse>? fp3 = null;
        if (raceWeek.FP3 is not null)
        {
            fp3 = new SessionResponse<FreePracticeResultResponse>(
                raceWeek.FP3.Start,
                raceWeek.FP3.SessionResults.Select(sr =>
                    _mapper.Map<FreePracticeResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<QualificationResultResponse>? sprintQualification = null;
        if (raceWeek.SprintQualifications is not null)
        {
            sprintQualification = new SessionResponse<QualificationResultResponse>(
                raceWeek.SprintQualifications.Start,
                raceWeek.SprintQualifications.SessionResults.Select(sr =>
                    _mapper.Map<QualificationResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<RaceResultResponse>? sprint = null;
        if (raceWeek.Sprint is not null)
        {
            sprint = new SessionResponse<RaceResultResponse>(
                raceWeek.Sprint.Start,
                raceWeek.Sprint.SessionResults.Select(sr =>
                    _mapper.Map<RaceResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<QualificationResultResponse>? raceQualification = null;
        if (raceWeek.RaceQualifications is not null)
        {
            raceQualification = new SessionResponse<QualificationResultResponse>(
                raceWeek.RaceQualifications.Start,
                raceWeek.RaceQualifications.SessionResults.Select(sr =>
                    _mapper.Map<QualificationResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        SessionResponse<RaceResultResponse>? race = null;
        if (raceWeek.Race is not null)
        {
            race = new SessionResponse<RaceResultResponse>(
                raceWeek.Race.Start,
                raceWeek.Race.SessionResults.Select(sr =>
                    _mapper.Map<RaceResultResponse>(
                        (sr, drivers.First(e => e.Id == sr.DriverId), teams.First(e => e.Id == sr.TeamId)))).ToList()
            );
        }

        var raceWeekResponse = new RaceWeekDetailResponse(
            raceWeek.Id.Value,
            raceWeek.Name,
            raceWeek.Slug,
            _mapper.Map<TrackResponse>(track),
            _mapper.Map<SeasonResponse>(season),
            fp1,
            fp2,
            fp3,
            sprintQualification,
            sprint,
            raceQualification,
            race);


        return raceWeekResponse;
    }

    private List<DriverId> GetAllDriverIds(RaceWeek raceWeek)
    {
        var driverIds = new List<DriverId>();

        if (raceWeek.FP1 is not null)
        {
            driverIds.AddRange(raceWeek.FP1.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.FP2 is not null)
        {
            driverIds.AddRange(raceWeek.FP2.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.FP3 is not null)
        {
            driverIds.AddRange(raceWeek.FP3.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.SprintQualifications is not null)
        {
            driverIds.AddRange(raceWeek.SprintQualifications.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.Sprint is not null)
        {
            driverIds.AddRange(raceWeek.Sprint.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.RaceQualifications is not null)
        {
            driverIds.AddRange(raceWeek.RaceQualifications.SessionResults.Select(e => e.DriverId));
        }

        if (raceWeek.Race is not null)
        {
            driverIds.AddRange(raceWeek.Race.SessionResults.Select(e => e.DriverId));
        }

        driverIds = driverIds.Distinct().ToList();
        return driverIds;
    }

    private List<TeamId> GetAllTeamIds(RaceWeek raceWeek)
    {
        var teamIds = new List<TeamId>();

        if(raceWeek.FP1 is not null)
        {
            teamIds.AddRange(raceWeek.FP1.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.FP2 is not null)
        {
            teamIds.AddRange(raceWeek.FP2.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.FP3 is not null)
        {
            teamIds.AddRange(raceWeek.FP3.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.SprintQualifications is not null)
        {
            teamIds.AddRange(raceWeek.SprintQualifications.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.Sprint is not null)
        {
            teamIds.AddRange(raceWeek.Sprint.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.RaceQualifications is not null)
        {
            teamIds.AddRange(raceWeek.RaceQualifications.SessionResults.Select(e => e.TeamId));
        }

        if (raceWeek.Race is not null)
        {
            teamIds.AddRange(raceWeek.Race.SessionResults.Select(e => e.TeamId));
        }

        teamIds = teamIds.Distinct().ToList();
        return teamIds;
    }
}
