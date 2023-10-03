using FluentValidation;

namespace Application.Features.Commands.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.genreName).NotEmpty();
    }
}
