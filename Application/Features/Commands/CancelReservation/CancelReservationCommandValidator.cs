using FluentValidation;

namespace Application.Features.Commands.CancelReservation;

public class CancelReservationCommandValidator : AbstractValidator<CancelReservationCommand>
{
    public CancelReservationCommandValidator()
    {
        RuleFor(x => x.MovieId).GreaterThan(0);
        RuleFor(x => x.NumberOfSeats).GreaterThan(0);
    }
}
