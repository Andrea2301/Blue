using Blue.Domain.Seasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blue.Infrastructure.Configurations;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SeasonNumber)
            .IsRequired();

        builder.Property(s => s.ReleaseYear)
            .IsRequired();

        // Season - Episodes
        builder
            .HasMany(s => s.Episodes)
            .WithOne()
            .HasForeignKey("SeasonId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
