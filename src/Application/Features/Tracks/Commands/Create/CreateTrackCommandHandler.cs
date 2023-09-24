using Application.Interfaces;
using Domain.Aggregates.Tracks;
using MediatR;

namespace Application.Features.Tracks.Commands.Create;
internal sealed class CreateTrackCommandHandler : IRequestHandler<CreateTrackCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateTrackCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
    {
        var track = Track.Create(
            request.Name,
            request.CountryCode,
            request.Image,
            request.DescriptionHtml);

        _unitOfWork.Tracks.Add(track);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
