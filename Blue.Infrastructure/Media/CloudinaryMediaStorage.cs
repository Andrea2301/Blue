using Blue.Application.Common.DTOs;
using Blue.Application.Common.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Blue.Infrastructure.Media;

public class CloudinaryMediaStorage : IMediaStorage
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryMediaStorage(IConfiguration config)
    {
        var account = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );

        _cloudinary = new Cloudinary(account);
    }

    public async Task<MediaUploadResult> UploadImageAsync(Stream file, string fileName)
    {
        var result = await _cloudinary.UploadAsync(
            new ImageUploadParams
            {
                File = new FileDescription(fileName, file)
            });

        return new MediaUploadResult(result.SecureUrl.ToString(), result.PublicId);
    }

    public async Task<MediaUploadResult> UploadVideoAsync(Stream file, string fileName)
    {
        var result = await _cloudinary.UploadAsync(
            new VideoUploadParams
            {
                File = new FileDescription(fileName, file)
            });

        return new MediaUploadResult(result.SecureUrl.ToString(), result.PublicId);
    }

    public Task DeleteAsync(string publicId)
        => _cloudinary.DestroyAsync(new DeletionParams(publicId));
}