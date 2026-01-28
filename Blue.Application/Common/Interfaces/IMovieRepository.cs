using Blue.Domain.Movies;

namespace Blue.Application.Common.Interfaces;

public interface IMovieRepository
{
    Task AddAsync(Movie movie);
}