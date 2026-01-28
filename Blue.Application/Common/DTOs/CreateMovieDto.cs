using System.IO;

namespace Blue.Application.Common.DTOs;

public class CreateMovieDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string AgeRating { get; set; } = default!;
    public int ReleaseYear { get; set; }

    // Cover
    public Stream CoverStream { get; set; } = default!;
    public string CoverFileName { get; set; } = default!;

    // Video
    public Stream VideoStream { get; set; } = default!;
    public string VideoFileName { get; set; } = default!;
}