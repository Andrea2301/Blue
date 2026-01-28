using Blue.Domain.Media;

namespace Blue.Application.Common.Interfaces;

public interface IMediaRepository
{
    Task AddAsync(MediaFile media);
    Task<MediaFile?> GetByIdAsync(Guid id);
    Task<List<MediaFile>> GetAllAsync();
    Task DeleteAsync(MediaFile media);
    Task UpdateAsync(MediaFile media);
}