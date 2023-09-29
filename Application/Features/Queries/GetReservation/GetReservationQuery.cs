using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetReservation;

public record GetReservationQuery(int Id) : IRequest<ReservationDto>;
