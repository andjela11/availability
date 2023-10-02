using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.FilterMovies;

public record FilterMoviesQuery(int PageSize, int PageNumber) : IRequest<List<MovieDto>>;

