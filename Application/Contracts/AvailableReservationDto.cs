namespace Application.Contracts;

public record AvailableReservationDto(string Title, int AvailableSeats)
{
    public static AvailableReservationDto FromMovieReservation(MovieDto movieDto, ReservationDto reservationDto)
    {
        return new AvailableReservationDto(movieDto.Title!, reservationDto.AvailableSeats);
    }
};
