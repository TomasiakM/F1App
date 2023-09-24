using Application.Interfaces;
using Domain.Aggregates.Seasons;
using MediatR;

namespace Application.Features.Seasons.Commands.Create;
internal sealed class CreateSeasonCommandHandler : IRequestHandler<CreateSeasonCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateSeasonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateSeasonCommand request, CancellationToken cancellationToken)
    {
        var season = Season.Create(request.Year);

        _unitOfWork.Seasons.Add(season);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
