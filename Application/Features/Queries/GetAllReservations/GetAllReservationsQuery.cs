using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetAllReservations;

public record GetAllReservationsQuery : IRequest<List<ReservationDto>>;
