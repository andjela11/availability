using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetReservation;

public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ReservationDto>
{
    private readonly IReservationHttpClient _httpClient;

    public GetReservationQueryHandler(IReservationHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ReservationDto> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservationDto = await _httpClient.GetReservationAsync(request.Id);

        if (reservationDto is null)
        {
            throw new EntityNotFoundException($"Reservation with id {request.Id} wasn't found");
        }

        return reservationDto;
    }
}
