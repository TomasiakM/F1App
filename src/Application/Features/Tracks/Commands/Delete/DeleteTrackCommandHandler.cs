using Application.Interfaces;
using Domain.Aggregates.Tracks.ValueObjects;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Tracks.Commands.Delete;
internal sealed class DeleteTrackCommandHandler : IRequestHandler<DeleteTrackCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTrackCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteTrackCommand request, CancellationToken cancellationToken)
    {
        var trackId = TrackId.Create(request.TrackId);
        var track = await _unitOfWork.Tracks.GetAsync(trackId);

        if(track is null)
        {
            throw new NotFoundException();
        }

        _unitOfWork.Tracks.Delete(track);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
