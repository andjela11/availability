using FluentValidation;

namespace Application.Features.Queries.ShowAvailableReservations;

public class ShowAvailableReservationsQueryValidator : AbstractValidator<ShowAvailableReservationsQuery>
{
    public ShowAvailableReservationsQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}
