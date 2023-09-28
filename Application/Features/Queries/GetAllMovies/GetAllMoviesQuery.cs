using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetAllMovies;

public record GetAllMoviesQuery() : IRequest<List<MovieDto>>;

