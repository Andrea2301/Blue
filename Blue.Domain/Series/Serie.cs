using Blue.Domain.Contents;
using Blue.Domain.Seasons;

namespace Blue.Domain.Series;

public class Series : Content
{
    public int StartYear { get; private set; }
    public int? EndYear { get; private set; }

    private readonly List<Season> _seasons = new();
    public IReadOnlyCollection<Season> Seasons => _seasons;

    protected Series() { }

    public Series(
        string title,
        string description,
        string ageRating,
        bool isOriginal,
        int startYear,
        string coverUrl)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        AgeRating = ageRating;
        IsOriginal = isOriginal;
        StartYear = startYear;
        CoverUrl = coverUrl;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddSeason(int seasonNumber, int releaseYear)
    {
        if (_seasons.Any(s => s.SeasonNumber == seasonNumber))
            throw new InvalidOperationException("La temporada ya existe");

        _seasons.Add(new Season(Id, seasonNumber, releaseYear));
        UpdatedAt = DateTime.UtcNow;
    }

    public void EndSeries(int endYear)
    {
        if (endYear < StartYear)
            throw new InvalidOperationException("El año final no puede ser menor al inicial");

        EndYear = endYear;
        UpdatedAt = DateTime.UtcNow;
    }
}