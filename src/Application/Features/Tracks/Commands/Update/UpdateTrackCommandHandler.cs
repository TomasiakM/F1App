using Application.Interfaces;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Tracks.Commands.Update;
internal sealed class UpdateTrackCommandHandler : IRequestHandler<UpdateTrackCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTrackCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateTrackCommand request, CancellationToken cancellationToken)
    {
        var trackId = TrackId.Create(request.TrackId);
        var track = await _unitOfWork.Tracks.GetAsync(trackId);

        if (track is null)
        {
            throw new NotFoundException();
        }

        track.Update(
            request.Name,
            request.CountryCode,
            request.Image,
            request.DescriptionHtml,
            request.Length,
            request.Corners,
            request.City);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
