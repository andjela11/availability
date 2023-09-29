using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetAllReservations;

public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDto>>
{
    private readonly IReservationHttpClient _httpClient;

    public GetAllReservationsQueryHandler(IReservationHttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _httpClient.GetAllReservationsAsync();
        return reservations ?? throw new EntityNotFoundException("No reservations");
    }
}
