using Blue.Application.Common.Interfaces;

namespace Blue.Application.Media.Commands;

public class DeleteMediaCommand
{
    private readonly IMediaRepository _repo;
    private readonly IMediaStorage _storage;

    public DeleteMediaCommand(IMediaRepository repo, IMediaStorage storage)
    {
        _repo = repo;
        _storage = storage;
    }

    public async Task ExecuteAsync(Guid id)
    {
        var media = await _repo.GetByIdAsync(id);
        if (media == null) throw new InvalidOperationException("Media not found");

        await _storage.DeleteAsync(media.PublicId);
        await _repo.DeleteAsync(media);
    }
}