using Application.Interfaces;
using Domain;
using Infrastructure.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class MongoDBService : IMongoDBService
{
    private readonly IMongoCollection<Genre> _genresCollection;
    
    public MongoDBService(IOptions<MongoDBSettings> mongoSettings)
    {
        MongoClient client = new MongoClient(mongoSettings.Value.ConnectionString);
        IMongoDatabase db = client.GetDatabase(mongoSettings.Value.DatabaseName);
        _genresCollection = db.GetCollection<Genre>(mongoSettings.Value.CollectionName);
    }
    
    public async Task<string> CreateGenre(string genreName)
    {
        var genre = new Genre { Name = genreName };
        await _genresCollection.InsertOneAsync(genre);
        return genre.Id;
    }
}
