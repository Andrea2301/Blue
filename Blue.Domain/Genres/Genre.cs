using Blue.Domain.Contents;

namespace Blue.Domain.Genres;

public class Genre
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;

    private readonly List<Content> _contents = new();
    public IReadOnlyCollection<Content> Contents => _contents;

    protected Genre() { }

    public Genre(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}