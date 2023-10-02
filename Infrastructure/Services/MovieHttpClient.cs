using System.Net.Http.Json;
using Application.Contracts;
using Application.Interfaces;
using Infrastructure.Helpers;
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
        var movieDto = await _httpClient.GetFromJsonAsync<MovieDto>($"{Constants.Controllers.Movies}/{id}");

        return movieDto;
    }

    public async Task<List<MovieDto>?> GetAllMoviesAsync()
    {
        var moviesDto = await _httpClient.GetFromJsonAsync<List<MovieDto>>(Constants.Controllers.Movies);

        return moviesDto;
    }
}
