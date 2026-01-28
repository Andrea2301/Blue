namespace Blue.Domain.Episodes;

public class Episode
{
    public Guid Id { get; private set; }
    public int EpisodeNumber { get; private set; }
    public string Title { get; private set; } = default!;
    public int DurationMinutes { get; private set; }

    public Guid SeasonId { get; private set; }

    protected Episode() { }

    internal Episode(int episodeNumber, string title, int durationMinutes)
    {
        Id = Guid.NewGuid();
        EpisodeNumber = episodeNumber;
        Title = title;
        DurationMinutes = durationMinutes;
    }
}