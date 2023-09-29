using FluentValidation;

namespace Application.Features.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        When(x => x.CreateReservationDto is not null, () =>
        {
            RuleFor(x => x.CreateReservationDto.MovieId).GreaterThan(0);
            RuleFor(x => x.CreateReservationDto.AvailableSeats).GreaterThan(0);
        });
    }
}
