using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.FilterMovies;

public class FilterMoviesQueryHandler : IRequestHandler<FilterMoviesQuery, List<MovieDto>>
{
    private readonly IMovieHttpClient _movieHttpClient;

    public FilterMoviesQueryHandler(IMovieHttpClient movieHttpClient)
    {
        _movieHttpClient = movieHttpClient;
    }

    public async Task<List<MovieDto>> Handle(FilterMoviesQuery request, CancellationToken cancellationToken)
    {
        var movieDtos = await _movieHttpClient.FilterMoviesAsync();
        return movieDtos ?? throw new EntityNotFoundException("No movies");
    }
}
