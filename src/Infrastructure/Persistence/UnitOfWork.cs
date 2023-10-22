using Application.Interfaces;
using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Teams;
using Domain.Aggregates.Tracks;
using Domain.Aggregates.Users;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IArticleRepository Articles { get; }
    public ICommentRepository Comments { get; }
    public IDriverRepository Drivers { get; }
    public IGeneralClassificationRepository GeneralClassifications { get; }
    public IRaceWeekRepository RaceWeeks { get; }
    public ISeasonRepository Seasons { get; }
    public ITagRepository Tags { get; }
    public ITeamRepository Teams { get; }
    public ITrackRepository Tracks { get; }
    public IUserRepository Users { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        Articles = new ArticleRepository(_dbContext);
        Comments = new CommentRepository(_dbContext);
        Drivers = new DriverRepository(_dbContext);
        GeneralClassifications = new GeneralClassificationRepository(_dbContext);
        RaceWeeks = new RaceWeekRepository(_dbContext);
        Seasons = new SeasonRepository(_dbContext);
        Tags = new TagRepository(_dbContext);
        Teams = new TeamRepository(_dbContext);
        Tracks = new TrackRepository(_dbContext);
        Users = new UserRepository(_dbContext);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
