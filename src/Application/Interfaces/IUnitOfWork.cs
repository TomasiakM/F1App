using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Teams;
using Domain.Aggregates.Tracks;
using Domain.Aggregates.Users;

namespace Application.Interfaces;
public interface IUnitOfWork
{
    IArticleRepository Articles { get; }
    ICommentRepository Comments { get; }
    IDriverRepository Drivers { get; }
    IRaceWeekRepository RaceWeeks { get; }
    ISeasonRepository Seasons { get; }
    ITagRepository Tags { get; }
    ITeamRepository Teams { get; }
    ITrackRepository Tracks { get; }
    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
