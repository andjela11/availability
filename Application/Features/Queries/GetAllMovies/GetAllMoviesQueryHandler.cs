using Application.Contracts;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetAllMovies;

public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<MovieDto>>
{
    private readonly IMovieHttpClient _movieHttpClient;

    public GetAllMoviesQueryHandler(IMovieHttpClient movieHttpClient)
    {
        _movieHttpClient = movieHttpClient;
    }
    
    public async Task<List<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        var movieDtos = await _movieHttpClient.GetAllMoviesAsync();
        return movieDtos ?? throw new Exception("No movies");
    }
}
