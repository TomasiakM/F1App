using Application.Interfaces;
using Domain.Aggregates.Drivers.ValueObjects;
using Domain.Aggregates.Ratings.Exceptions;
using Domain.Aggregates.Ratings.ValueObjects;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.UserDriverRatings.Exceptions;
using Domain.Exceptions;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Ratings.Commands.AddRatings;
internal sealed class AddRatingsCommandHandler : IRequestHandler<AddRatingsCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IDateProvider _dateProvider;

    public AddRatingsCommandHandler(IUnitOfWork unitOfWork, IUserService userService, IDateProvider dateProvider)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(AddRatingsCommand request, CancellationToken cancellationToken)
    {
        var ratingId = RatingId.Create(request.RatingId);
        var rating = await _unitOfWork.Ratings.GetAsync(ratingId, cancellationToken);

        if (rating is null)
        {
            throw new NotFoundException();
        }

        if (_dateProvider.Now > rating.Finish)
        {
            throw new RatingIsFinishedException();
        }

        var userId = _userService.GetUserId();
        var userRatings = await _unitOfWork.UserDriverRatings
            .FindAsync(e => e.RatingId == rating.Id && e.UserId == userId);

        if (userRatings is not null)
        {
            throw new UserRatedDriversException();
        }

        var driverRatings = request.Ratings
            .Select(e =>
                UserDriverRating.Create(
                    rating.Id,
                    DriverId.Create(new(e.DriverId)),
                    userId,
                    e.Rating))
            .ToList();

        rating.AddRatings(driverRatings);

        _unitOfWork.UserDriverRatings.AddRange(driverRatings);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
