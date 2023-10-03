using FluentValidation;

namespace Application.Features.Commands.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(x => x.Genre).NotNull();
        When(x => x.Genre is not null, () =>
        {
            RuleFor(x => x.Genre.Id).NotEmpty();
            RuleFor(x => x.Genre.Name).NotEmpty();
        });
    }
}
