using Blue.Domain.Contents;

namespace Blue.Domain.Movies;

public class Movie : Content
{
    public int ReleaseYear { get; private set; }
    public string VideoUrl { get; private set; } = default!;

    protected Movie() { } // EF Core

    private Movie(
        string title,
        string description,
        string ageRating,
        int releaseYear,
        string coverUrl,
        string videoUrl)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        AgeRating = ageRating;
        ReleaseYear = releaseYear;
        CoverUrl = coverUrl;
        VideoUrl = videoUrl;
        IsOriginal = false;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    
    public static Movie Create(
        string title,
        string description,
        string ageRating,
        int releaseYear,
        string coverUrl,
        string videoUrl)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");

        if (string.IsNullOrWhiteSpace(videoUrl))
            throw new ArgumentException("VideoUrl is required");

        return new Movie(
            title,
            description,
            ageRating,
            releaseYear,
            coverUrl,
            videoUrl
        );
    }
}