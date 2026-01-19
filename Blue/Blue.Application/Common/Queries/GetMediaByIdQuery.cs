using Blue.Application.Common.Interfaces;
using Blue.Application.Media.DTOs;

public class GetMediaByIdQuery
{
    private readonly IMediaRepository _repo;

    public GetMediaByIdQuery(IMediaRepository repo)
    {
        _repo = repo;
    }

    public async Task<MediaResponse?> ExecuteAsync(Guid id)
    {
        var media = await _repo.GetByIdAsync(id);
        return media == null ? null : new MediaResponse(media.Id, media.Url, media.Type);
    }
}