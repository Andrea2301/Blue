using Blue.Application.Common.DTOs;
using Blue.Application.Media.DTOs;

namespace Blue.Application.Common.Interfaces;

public interface IMediaStorage
{
    
    Task<MediaUploadResult> UploadImageAsync(Stream file, string fileName);
    Task<MediaUploadResult> UploadVideoAsync(Stream file, string fileName);
    Task DeleteAsync(string publicId);
}