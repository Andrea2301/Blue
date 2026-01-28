using Blue.Application.Common.Interfaces;
using Blue.Application.Media.DTOs;

public class GetAllMediaQuery
{
    private readonly IMediaRepository _repo;

    public GetAllMediaQuery(IMediaRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<MediaResponse>> ExecuteAsync()
    {
        var media = await _repo.GetAllAsync();
        return media.Select(m => new MediaResponse(m.Id, m.Url, m.Type)).ToList();
    }
}