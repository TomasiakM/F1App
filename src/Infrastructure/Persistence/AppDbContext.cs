using Domain.Aggregates.Articles;
using Domain.Aggregates.Comments;
using Domain.Aggregates.Drivers;
using Domain.Aggregates.DriverStatistics;
using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.RaceWeeks;
using Domain.Aggregates.Ratings;
using Domain.Aggregates.Roles;
using Domain.Aggregates.Seasons;
using Domain.Aggregates.Tags;
using Domain.Aggregates.Teams;
using Domain.Aggregates.TeamStatistics;
using Domain.Aggregates.Tracks;
using Domain.Aggregates.UserDriverRatings;
using Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Persistence;
internal sealed class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<DriverStatistic> DriverStatistics => Set<DriverStatistic>();
    public DbSet<GeneralClassification> GeneralClassification => Set<GeneralClassification>();
    public DbSet<RaceWeek> RaceWeeks => Set<RaceWeek>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamStatistic> TeamStatistics => Set<TeamStatistic>();
    public DbSet<Track> Tracks => Set<Track>();
    public DbSet<UserDriverRating> UserDriverRatings => Set<UserDriverRating>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("DbConnection"));
        //options.EnableSensitiveDataLogging()
        //   .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
