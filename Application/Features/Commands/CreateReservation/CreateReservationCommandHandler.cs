using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IMovieHttpClient _movieHttpClient;
    private readonly IReservationHttpClient _reservationHttpClient;

    public CreateReservationCommandHandler(IMovieHttpClient movieHttpClient, IReservationHttpClient reservationHttpClient)
    {
        _movieHttpClient = movieHttpClient;
        _reservationHttpClient = reservationHttpClient;
    }

    public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationHttpClient.GetReservationAsync(request.CreateReservationDto.MovieId);

        if (reservation is not null)
        {
            if (request.CreateReservationDto.NumberOfSeats > reservation.AvailableSeats)
            {
                throw new NoAvailableSeatsException(
                    $"There is no {request.CreateReservationDto.NumberOfSeats} seats available! You can reserve {reservation.AvailableSeats} max");
            }
            
            reservation = reservation with { AvailableSeats = reservation.AvailableSeats - request.CreateReservationDto.NumberOfSeats };
            await _reservationHttpClient.UpdateReservationAsync(reservation);
            return reservation.Id;
        }

        var movieDto = await _movieHttpClient.GetMovieAsync(request.CreateReservationDto.MovieId);

        if (movieDto is null)
        {
            throw new EntityNotFoundException($"Movie with id {request.CreateReservationDto.MovieId} wasn't found!");
        }

        var reservationId = await _reservationHttpClient.CreateReservationAsync(request.CreateReservationDto);
        return reservationId;
    }
}
