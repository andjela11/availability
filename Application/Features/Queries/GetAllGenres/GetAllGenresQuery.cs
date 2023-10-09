using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetAllGenres;

public record GetAllGenresQuery() : IRequest<List<GenreDto>>;
