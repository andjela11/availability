namespace Application.Interfaces;

public interface IMongoDBService
{
    public Task<string> CreateGenre(string genreName);
}
