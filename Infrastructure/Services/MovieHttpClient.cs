using System.Text.Json;
using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class MovieHttpClient : IMovieHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediator;

    public MovieHttpClient(HttpClient httpClient, IConfiguration configuration, IMediator mediator)
    {
        _httpClient = httpClient;
        _mediator = mediator;
        var uri = new Uri(configuration.GetSection("ApiUrls:MovieApiBaseUrl").Value);
        _httpClient.BaseAddress = uri;
        // _httpClient.BaseAddress = new Uri("http://localhost:5180/Movies/");
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
}