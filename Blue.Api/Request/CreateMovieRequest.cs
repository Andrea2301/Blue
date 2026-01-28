namespace Blue.Api.Controllers.Requests;

public class CreateMovieRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public int ReleaseYear { get; set; }

    public IFormFile Cover { get; set; } = default!;
    public IFormFile Video { get; set; } = default!;
}