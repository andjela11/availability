using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
{
    private readonly IReservationHttpClient _reservationHttpClient;

    public CreateReservationCommandHandler(IReservationHttpClient reservationHttpClient)
    {
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

        var reservationId = await _reservationHttpClient.CreateReservationAsync(request.CreateReservationDto);
        return reservationId;
    }
}
