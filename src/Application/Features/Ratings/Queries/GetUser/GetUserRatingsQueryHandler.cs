using Application.Dtos.Rating.Responses;
using Application.Interfaces;
using Domain.Aggregates.Ratings.ValueObjects;
using MapsterMapper;
using MediatR;

namespace Application.Features.Ratings.Queries.GetUser;
internal sealed class GetUserRatingsQueryHandler : IRequestHandler<GetUserRatingsQuery, UserDriverRatingsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserRatingsQueryHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<UserDriverRatingsResponse> Handle(GetUserRatingsQuery request, CancellationToken cancellationToken)
    {
        var ratingId = RatingId.Create(request.RatingId);
        var userId = _userService.GetUserId();

        var userRatings = await _unitOfWork.UserDriverRatings
            .FindAllAsync(e => e.RatingId == ratingId && e.UserId == userId, cancellationToken);

        var userRatingDtos = _mapper.Map<List<UserDriverRatingResponse>>(userRatings);

        return new(
            userId.Value,
            userRatingDtos);
    }
}
