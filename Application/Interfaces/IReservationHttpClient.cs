using Application.Contracts;

namespace Application.Interfaces;

public interface IReservationHttpClient
{
    Task<int> CreateReservationAsync(CreateReservationDto createReservationDto);
    Task UpdateReservationAsync(ReservationDto updateReservationDto);
    Task<ReservationDto?> GetReservationAsync(int id);
    Task<List<ReservationDto>?> GetAllReservationsAsync();
}
