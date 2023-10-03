using MediatR;

namespace Application.Features.Commands.DeleteGenre;

public record DeleteGenreCommand(string GenreId) : IRequest<Unit>;
