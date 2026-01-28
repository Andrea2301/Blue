using Blue.Application.Common.Interfaces;
using Blue.Domain;
using Blue.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Blue.Infrastructure.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MediaFile media)
    {
        _context.MediaFiles.Add(media);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MediaFile>> GetAllAsync()
    {
        return await _context.MediaFiles.ToListAsync();
    }

    public async Task<MediaFile?> GetByIdAsync(Guid id)
    {
        return await _context.MediaFiles.FindAsync(id);
    }

    public async Task DeleteAsync(MediaFile media)
    {
        _context.MediaFiles.Remove(media);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MediaFile media)
    {
        _context.MediaFiles.Update(media);
        await _context.SaveChangesAsync();
    }
}