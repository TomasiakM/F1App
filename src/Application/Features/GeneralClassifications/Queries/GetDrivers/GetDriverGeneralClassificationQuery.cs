using Application.Dtos.GeneralClassification.Responses;
using MediatR;

namespace Application.Features.GeneralClassifications.Queries.GetDrivers;
public record GetDriverGeneralClassificationQuery(
    int Year) : IRequest<List<DriverGeneralClassificationResponse>>;
