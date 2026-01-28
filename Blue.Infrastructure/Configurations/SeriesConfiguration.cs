using Blue.Domain.Series;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blue.Infrastructure.Configurations;

public class SeriesConfiguration : IEntityTypeConfiguration<Series>
{
    public void Configure(EntityTypeBuilder<Series> builder)
    {
        builder.Property(s => s.StartYear)
            .IsRequired();

        builder.Property(s => s.EndYear)
            .IsRequired(false);

        //  Series - Seasons (Aggregate)
        builder
            .HasMany(s => s.Seasons)
            .WithOne()
            .HasForeignKey("SeriesId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}