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
        var httpResponseMessage = await _httpClient.PostAsJsonAsync(Constants.Controllers.Reservations, createReservationDto);
        var content = await httpResponseMessage.Content.ReadAsStringAsync();

        var reservationId = JsonSerializer.Deserialize<int>(content, options);

        return reservationId;
    }

    public async Task UpdateReservationAsync(ReservationDto updateReservationDto)
    {
        await _httpClient.PutAsJsonAsync(Constants.Controllers.Reservations, updateReservationDto);
    }

    public async Task<ReservationDto?> GetReservationAsync(int id)
    {
        var reservationDto = await _httpClient
            .GetFromJsonAsync<ReservationDto>($"{Constants.Controllers.Reservations}/get-by-movieid/{id}");

        return reservationDto;
    }

    public async Task<List<ReservationDto>?> GetAllReservationsAsync()
    {
        var reservationsDto = await _httpClient
            .GetFromJsonAsync<List<ReservationDto>>(Constants.Controllers.Reservations);

        return reservationsDto;
    }
}
