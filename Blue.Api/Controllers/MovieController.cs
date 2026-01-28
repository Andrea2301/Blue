using Blue.Api.Controllers.Requests;
using Blue.Application.Common.DTOs;
using Blue.Application.Media.Commands;
using Blue.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blue.Api.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CreateMovieCommand _create;

    public MoviesController(
        AppDbContext context,
        CreateMovieCommand create)
    {
        _context = context;
        _create = create;
    }

    // CREAR (Admin)
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create(
        [FromForm] CreateMovieRequest request)
    {
        var dto = new CreateMovieDto
        {
            Title = request.Title,
            Description = request.Description,
            AgeRating = request.AgeRating,
            ReleaseYear = request.ReleaseYear,

            CoverStream = request.Cover.OpenReadStream(),
            CoverFileName = request.Cover.FileName,

            VideoStream = request.Video.OpenReadStream(),
            VideoFileName = request.Video.FileName
        };

        var movie = await _create.ExecuteAsync(dto);
        return Ok(movie);
    }


    // VER (Usuarios)
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _context.Movies
            .Select(m => new
            {
                m.Id,
                m.Title,
                m.Description,
                m.CoverUrl,
                m.VideoUrl,
                m.ReleaseYear
            })
            .ToListAsync();

        return Ok(movies);
    }
}