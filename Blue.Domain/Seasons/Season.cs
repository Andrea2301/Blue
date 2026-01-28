using Blue.Domain.Episodes;

namespace Blue.Domain.Seasons;

public class Season
{
    public Guid Id { get; private set; }
    public int SeasonNumber { get; private set; }
    public int ReleaseYear { get; private set; }

    public Guid SeriesId { get; private set; }

    private readonly List<Episode> _episodes = new();
    public IReadOnlyCollection<Episode> Episodes => _episodes;

    protected Season() { }

    public Season(Guid seriesId, int seasonNumber, int releaseYear)
    {
        Id = Guid.NewGuid();
        SeriesId = seriesId;
        SeasonNumber = seasonNumber;
        ReleaseYear = releaseYear;
    }

    public void AddEpisode(int episodeNumber, string title, int durationMinutes)
    {
        if (_episodes.Any(e => e.EpisodeNumber == episodeNumber))
            throw new InvalidOperationException("El episodio ya existe en esta temporada");

        _episodes.Add(new Episode(episodeNumber,title, durationMinutes));
    }
}