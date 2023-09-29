using Application.Contracts;
using MediatR;

namespace Application.Features.Commands.CreateReservation;

public record CreateReservationCommand(CreateReservationDto CreateReservationDto) : IRequest<int>;
