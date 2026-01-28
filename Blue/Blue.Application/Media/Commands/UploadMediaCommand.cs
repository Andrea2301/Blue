using Blue.Application.Common.Interfaces;
using Blue.Application.Common.DTOs;
using Blue.Application.Media.DTOs;
using Blue.Domain.Common;
using Blue.Domain.Media;

namespace Blue.Application.Media.Commands;

public class UploadMediaCommand
{
    private readonly IMediaStorage _storage;
    private readonly IMediaRepository _repository;

    public UploadMediaCommand(
        IMediaStorage storage,
        IMediaRepository repository)
    {
        _storage = storage;
        _repository = repository;
    }

    public async Task<MediaResponse> ExecuteAsync(
        Stream file,
        string fileName,
        string contentType)
    {
        var isVideo = contentType.StartsWith("video");

        var upload = isVideo
            ? await _storage.UploadVideoAsync(file, fileName)
            : await _storage.UploadImageAsync(file, fileName);

        var media = MediaFile.Create(
            upload.Url,
            upload.PublicId,
            isVideo ? "video" : "image");

        await _repository.AddAsync(media);

        return new MediaResponse(media.Id, media.Url, media.Type);
    }
}