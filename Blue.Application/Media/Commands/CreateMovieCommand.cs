using Blue.Application.Common.DTOs;
using Blue.Application.Common.Interfaces;
using Blue.Domain.Movies;

namespace Blue.Application.Media.Commands;

public class CreateMovieCommand
{
    private readonly IMovieRepository _repository;
    private readonly ICloudinaryService _cloudinary;

    public CreateMovieCommand(
        IMovieRepository repository,
        ICloudinaryService cloudinary)
    {
        _repository = repository;
        _cloudinary = cloudinary;
    }

    public async Task<Movie> ExecuteAsync(CreateMovieDto dto)
    {
        // upload files
        var coverUrl = await _cloudinary.UploadImageAsync(
            dto.CoverStream,
            dto.CoverFileName);

        var videoUrl = await _cloudinary.UploadVideoAsync(
            dto.VideoStream,
            dto.VideoFileName);

        // 
        var movie = Movie.Create(
            dto.Title,
            dto.Description,
            dto.AgeRating,
            dto.ReleaseYear,
            coverUrl,
            videoUrl);

        // 
        await _repository.AddAsync(movie);

        return movie;
    }
}