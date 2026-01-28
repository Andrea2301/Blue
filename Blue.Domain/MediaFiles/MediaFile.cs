using Blue.Domain.Common;

namespace Blue.Domain;

public class MediaFile : BaseEntity<Guid>
{
    public string Url { get; private set; } = default!;
    public string PublicId { get; private set; } = default!;
    public string Type { get; private set; } = default!; // image | video

    private MediaFile() { }

    private MediaFile(string url, string publicId, string type)
    {
        Url = url;
        PublicId = publicId;
        Type = type;
    }

    public static MediaFile Create(string url, string publicId, string type)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Url is required");

        if (string.IsNullOrWhiteSpace(publicId))
            throw new ArgumentException("PublicId is required");

        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type is required");

        return new MediaFile(url, publicId, type);
    }

    
    public void Update(string url, string publicId, string type)
    {
        Url = url;
        PublicId = publicId;
        Type = type;
    }
}