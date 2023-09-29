using System.Text.Json;
using Application.Contracts;
using Application.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class MovieHttpClient : IMovieHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public MovieHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var uri = new Uri(configuration.GetSection("ApiUrls:MovieApiBaseUrl").Value);
        _httpClient.BaseAddress = uri;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }


    public async Task<MovieDto?> GetMovieAsync(int id)
    {
        var content = await SendRequest($"{Constants.Controllers.MoviesController}/{id}");

        var movieDto = JsonSerializer.Deserialize<MovieDto>(content, _options);

        return movieDto;
    }

    public async Task<List<MovieDto>?> GetAllMoviesAsync()
    {
        var content = await SendRequest(Constants.Controllers.MoviesController);

        var moviesDto = JsonSerializer.Deserialize<List<MovieDto>>(content, _options);

        return moviesDto;
    }

    private async Task<string> SendRequest(string requestUri)
    {
        var httpResponseMessage = await _httpClient.GetAsync(requestUri);
        return await httpResponseMessage.Content.ReadAsStringAsync();
    }

}
