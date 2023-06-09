using Application.Dtos.Tag.Requests;
using Application.Dtos.Tag.Responses;
using Application.Features.Tags.Commands.Create;
using Domain.Aggregates.Tags;
using Mapster;

namespace Application.Mapper;
internal class TagMapperConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTagRequest, CreateTagCommand>();

        config.NewConfig<Tag, TagResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}
