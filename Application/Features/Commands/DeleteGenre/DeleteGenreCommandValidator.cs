using FluentValidation;

namespace Application.Features.Commands.DeleteGenre;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(x => x.GenreId).NotEmpty();
    }
}
