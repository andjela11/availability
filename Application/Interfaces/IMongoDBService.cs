using Application.Contracts;
using Domain;
using MongoDB.Driver;

namespace Application.Interfaces;

public interface IMongoDBService
{
    public Task<string> CreateGenre(string genreName);

    public Task<List<GenreDto>> GetAllGenres();

    public Task UpdateGenre(Genre genre);

    public Task DeleteGenre(string id);
}
