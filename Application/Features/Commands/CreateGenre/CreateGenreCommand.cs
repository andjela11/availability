
using MediatR;

namespace Application.Features.Commands.CreateGenre;

public record CreateGenreCommand(string GenreName) : IRequest<string>;
