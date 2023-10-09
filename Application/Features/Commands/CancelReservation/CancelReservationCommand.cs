using MediatR;

namespace Application.Features.Commands.CancelReservation;

public record CancelReservationCommand(int MovieId, int NumberOfSeats) : IRequest<Unit>;
