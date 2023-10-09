using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Commands.CancelReservation;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, Unit>
{
    private readonly IReservationHttpClient _reservationHttpClient;

    public CancelReservationCommandHandler(IReservationHttpClient reservationHttpClient)
    {
        _reservationHttpClient = reservationHttpClient;
    }
    
    public async Task<Unit> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationHttpClient.GetReservationAsync(request.MovieId);

        if (reservation is null)
        {
            throw new EntityNotFoundException("Reservation doesn't exist");
        }

        reservation = reservation with {AvailableSeats = reservation.AvailableSeats + request.NumberOfSeats};

        var reservationDto = new ReservationDto(reservation.Id, request.MovieId,
            reservation.AvailableSeats);
        await _reservationHttpClient.UpdateReservationAsync(reservationDto);
        
        return new Unit();
    }
}
