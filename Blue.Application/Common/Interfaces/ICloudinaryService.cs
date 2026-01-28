namespace Blue.Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(Stream stream, string fileName);
    Task<string> UploadVideoAsync(Stream stream, string fileName);
}