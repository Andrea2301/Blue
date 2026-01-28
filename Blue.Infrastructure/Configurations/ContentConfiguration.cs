using Blue.Domain.Contents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title).IsRequired();
        builder.Property(c => c.Description).IsRequired();
        builder.Property(c => c.AgeRating).IsRequired();
        builder.Property(c => c.CoverUrl).IsRequired();
    }
}