using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.ShowAvailableReservations;

public record ShowAvailableReservationsQuery(int PageNumber, int PageSize) : IRequest<List<AvailableReservationDto>>;
