using Application.Dtos.Auth.Requests;
using Application.Dtos.User.Requests;
using Application.Features.User.Commands;
using Application.Features.User.Commands.Register;
using Application.Features.User.Commands.UpdatePassword;
using Mapster;

namespace Application.Mapper;
internal sealed class UserMapperConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<UpdatePasswordRequest, UpdatePasswordCommand>();

        config.NewConfig<(Guid userId, BanUserRequest request), BanUserCommand>()
            .Map(dest => dest.UserId, src => src.userId)
            .Map(dest => dest, src => src.request);
    }
}
