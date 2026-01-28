using Blue.Domain.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blue.Infrastructure.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

   

        builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.AgeRating).IsRequired();
        builder.Property(x => x.ReleaseYear).IsRequired();
        builder.Property(x => x.CoverUrl).IsRequired();
        builder.Property(x => x.VideoUrl).IsRequired();
    }
}