using Application.Contracts;
using Application.Exceptions;
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

    public async Task<List<GenreDto>> GetAllGenres()
    {
        var genres = await _genresCollection.Find(_ => true).ToListAsync();

        return genres.Select(GenreDto.FromGenre).ToList();
    }

    public async Task UpdateGenre(Genre genre)
    {
        var filter = Builders<Genre>.Filter.Eq(g => g.Id, genre.Id);
        var update = Builders<Genre>.Update.Set(g => g.Name, genre.Name);
        
        var r = _genresCollection.UpdateOneAsync(filter, update, new UpdateOptions()
        {
            IsUpsert = false
        }).Result;
        if (r.MatchedCount is 0)
        {
            throw new EntityNotFoundException("Entity not found");
        }
        
        if (r.ModifiedCount is 0)
        {
            throw new Exception("No entity was modified");
        }
    }

    public async Task DeleteGenre(string id)
    {
        var filter = Builders<Genre>.Filter.Eq(g => g.Id, id);
        var result = _genresCollection.DeleteOneAsync(filter).Result;
        
        if (result.DeletedCount is 0)
        {
            throw new EntityNotFoundException("Entity was not deleted");
        }
    }
}
