using System.Net.Http.Json;
using System.Text.Json;
using Application.Contracts;
using Application.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class ReservationHttpClient : IReservationHttpClient
{
    private readonly HttpClient _httpClient;
    private JsonSerializerOptions options;

    public ReservationHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var uri = new Uri(configuration.GetSection("ApiUrls:ReservationApiBaseUrl").Value);
        _httpClient.BaseAddress = uri;
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task<int> CreateReservationAsync(CreateReservationDto createReservationDto)
    {
        var httpContent = JsonContent.Create(createReservationDto);
        var httpResponseMessage = await _httpClient.PostAsync(Constants.Controllers.ReservationsController, httpContent);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();

        var reservationId = JsonSerializer.Deserialize<int>(content, options);

        return reservationId;
    }

    public async Task<ReservationDto?> GetReservationAsync(int id)
    {
        var httpResponseMessage = await _httpClient.GetAsync($"{Constants.Controllers.ReservationsController}/{id}");
        var content = await httpResponseMessage.Content.ReadAsStringAsync();

        var reservationDto = JsonSerializer.Deserialize<ReservationDto>(content, options);

        return reservationDto;
    }
}
