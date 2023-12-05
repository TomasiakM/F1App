using Application.Dtos.DriverStatistic.Responses;
using MediatR;

namespace Application.Features.DriverStatistics.Queries.Get;
public record GetDriverStatisticQuery(
    Guid DriverId) : IRequest<DriverStatisticResponse>;
