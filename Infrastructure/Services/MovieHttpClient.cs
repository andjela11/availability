using System.Text.Json;
using Application.Contracts;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class MovieHttpClient : IMovieHttpClient
{
    private readonly HttpClient _httpClient;

    public MovieHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var uri = new Uri(configuration.GetSection("ApiUrls:MovieApiBaseUrl").Value);
        _httpClient.BaseAddress = uri;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }


    public async Task<MovieDto?> GetMovieAsync(int id)
    {
        var httpResponseMessage = await _httpClient.GetAsync($"Movies/{id}");
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var movieDto = JsonSerializer.Deserialize<MovieDto>(content, opt);

        return movieDto;
    }

    public async Task<List<MovieDto>?> GetAllMoviesAsync()
    {
        var httpResponseMessage = await _httpClient.GetAsync($"Movies");
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var opt = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var moviesDto = JsonSerializer.Deserialize<List<MovieDto>>(content, opt);

        return moviesDto;
    }
}
