using Application.Dtos.GeneralClassification.Responses;
using MediatR;

namespace Application.Features.GeneralClassifications.Queries.GetTeams;
public record GetTeamGeneralClassificationQuery(
    int Year) : IRequest<List<TeamGeneralClassificationResponse>>;
