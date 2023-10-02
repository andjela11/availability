using FluentValidation;

namespace Application.Features.Queries.ShowAvailableReservations;

public class ShowAvailableReservationsQueryValidator : AbstractValidator<ShowAvailableReservationsQuery>
{
    public ShowAvailableReservationsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
