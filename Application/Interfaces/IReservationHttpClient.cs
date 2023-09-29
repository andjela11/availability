using Application.Contracts;

namespace Application.Interfaces;

public interface IReservationHttpClient
{
    Task<int> CreateReservationAsync(CreateReservationDto createReservationDto);
    Task<ReservationDto?> GetReservationAsync(int id);
}
