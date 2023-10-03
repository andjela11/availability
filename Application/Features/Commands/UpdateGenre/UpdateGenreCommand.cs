using Domain;
using MediatR;

namespace Application.Features.Commands.UpdateGenre;

public record UpdateGenreCommand(Genre Genre) : IRequest;

