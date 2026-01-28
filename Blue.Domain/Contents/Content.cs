using Blue.Domain.Genres;

namespace Blue.Domain.Contents;

public abstract class Content
{
    //protected set permite que Movie y Series lo controlen sin exponer setters
    public Guid Id { get; protected set; }
    public string Title { get; protected set; } = default!;
    public string Description { get; protected set; } = default!;
    public string AgeRating { get; protected set; } = default!;
    public bool IsOriginal { get; protected set; } 
    public string CoverUrl { get; set; } = default!;
    public DateTime CreatedAt { get; protected set; } 
    public DateTime UpdatedAt { get; protected set; }

    public List<Genre> Genres { get; private set; } = new List<Genre>();


    public void AddGenre(Genre genre)
    {
        if (!Genres.Contains(genre))
            Genres.Add(genre);
    }


}