using Blue.Application.Common.Interfaces;
using Blue.Application.Media.DTOs;

namespace Blue.Application.Media.Commands;

public class UpdateMediaCommand
{
    private readonly IMediaRepository _repo;
    private readonly IMediaStorage _storage;

    public UpdateMediaCommand(IMediaRepository repo, IMediaStorage storage)
    {
        _repo = repo;
        _storage = storage;
    }

    public async Task<MediaResponse> ExecuteAsync(Guid id, Stream file, string fileName, string contentType)
    {
        var media = await _repo.GetByIdAsync(id);
        if (media == null) throw new InvalidOperationException("Media not found");

        // borrar el anterior
        await _storage.DeleteAsync(media.PublicId);

        // subir nuevo
        var isVideo = contentType.StartsWith("video");
        var upload = isVideo
            ? await _storage.UploadVideoAsync(file, fileName)
            : await _storage.UploadImageAsync(file, fileName);

        // actualizar entidad
        media.Update(upload.Url, upload.PublicId, isVideo ? "video" : "image");

        await _repo.UpdateAsync(media);

        return new MediaResponse(media.Id, media.Url, media.Type);
    }
    
    
}