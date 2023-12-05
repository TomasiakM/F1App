using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Teams;
using Domain.Aggregates.TeamStatistics;
using Domain.Aggregates.Tracks;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.Users;

namespace Application.Interfaces;
public interface IUnitOfWork
{
    IArticleRepository Articles { get; }
    ICommentRepository Comments { get; }
    IDriverRepository Drivers { get; }
    IDriverStatisticRepository DriverStatistics { get; }
    IGeneralClassificationRepository GeneralClassifications { get; }
    IRaceWeekRepository RaceWeeks { get; }
    IRatingRepository Ratings { get; }
    ISeasonRepository Seasons { get; }
    ITagRepository Tags { get; }
    ITeamRepository Teams { get; }
    ITeamStatisticRepository TeamStatistics { get; }
    ITrackRepository Tracks { get; }
    IUserDriverRatingRepository UserDriverRatings { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
