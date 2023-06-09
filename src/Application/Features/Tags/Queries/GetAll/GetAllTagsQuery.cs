using Application.Dtos.Tag.Responses;
using MediatR;

namespace Application.Features.Tags.Queries.GetAll;
public record GetAllTagsQuery() : IRequest<List<TagResponse>>;
