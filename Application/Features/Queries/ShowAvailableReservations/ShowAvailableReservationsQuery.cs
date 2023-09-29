using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.ShowAvailableReservations;

public record ShowAvailableReservationsQuery() : IRequest<List<AvailableReservationDto>>;
