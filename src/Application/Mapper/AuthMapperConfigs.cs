using Application.Dtos.Auth.Requests;
using Application.Features.Auth.Queries.Login;
using Mapster;

namespace Application.Mapper.Auth;
internal sealed class AuthMapperConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>();
    }
}
