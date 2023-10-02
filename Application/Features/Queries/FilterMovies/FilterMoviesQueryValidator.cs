using FluentValidation;

namespace Application.Features.Queries.FilterMovies;

public class FilterMoviesQueryValidator : AbstractValidator<FilterMoviesQuery>
{
    public FilterMoviesQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(0);
    }
}
