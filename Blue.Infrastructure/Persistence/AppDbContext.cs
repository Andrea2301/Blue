using Blue.Domain;
using Blue.Domain.Contents;
using Blue.Domain.Episodes;
using Blue.Domain.Genres;
using Blue.Domain.Movies;
using Blue.Domain.Seasons;
using Blue.Domain.Series;
using Blue.Domain.Subscriptions;
using Blue.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Blue.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Series> Series => Set<Series>();
    public DbSet<Season> Seasons => Set<Season>();
    public DbSet<Episode> Episodes => Set<Episode>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<MediaFile> MediaFiles => Set<MediaFile>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔹 DDD: aplicar configuraciones separadas
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);

        // 🔹 Herencia Content → Movie / Series
        modelBuilder.Entity<Content>()
            .UseTptMappingStrategy();

        modelBuilder.Entity<Movie>()
            .ToTable("Movies");

        modelBuilder.Entity<Series>()
            .ToTable("Series");
    }
}