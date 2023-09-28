using Application.Contracts;

namespace Application.Interfaces;

public interface IMovieHttpClient
{
    Task<MovieDto?> GetMovieAsync(int id);
}