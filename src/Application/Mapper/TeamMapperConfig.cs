using Application.Dtos.Team.Requests;
using Application.Dtos.Team.Responses;
using Application.Features.Teams.Commands.Create;
using Application.Features.Teams.Commands.Update;
using Domain.Aggregates.Teams;
using Mapster;

namespace Application.Mapper;
internal sealed class TeamMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTeamRequest, CreateTeamCommand>();

        config.NewConfig<(Guid teamId, UpdateTeamRequest request), UpdateTeamCommand>()
            .Map(dest => dest.TeamId, src => src.teamId)
            .Map(dest => dest, src => src.request);

        config.NewConfig<Team, TeamResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
