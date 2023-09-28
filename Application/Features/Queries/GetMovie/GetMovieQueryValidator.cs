using FluentValidation;

namespace Application.Features.Queries.GetMovie;

public class GetMovieQueryValidator : AbstractValidator<GetMovieQuery>
{
    public GetMovieQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}