using Application.Contracts;
using MediatR;

namespace Application.Features.Queries.GetMovie;

public record GetMovieQuery(int Id) : IRequest<MovieDto?>;
