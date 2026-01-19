namespace Blue.Application.Media.DTOs;

public class MediaResponse
{
    public Guid Id { get; init; }
    public string Url { get; init; }
    public string Type { get; init; }

    public MediaResponse(Guid id, string url, string type)
    {
        Id = id;
        Url = url;
        Type = type;
    }
}