using Application.Contracts;
using Application.Exceptions;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Queries.GetMovie;

public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieDto?>
{
    private readonly IMovieHttpClient _httpClient;

    public GetMovieQueryHandler(IMovieHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MovieDto?> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        // return default;
        var movieDto = await _httpClient.GetMovieAsync(request.Id);

        if (movieDto is null)
        {
            throw new EntityNotFoundException($"Movie with id {request.Id} wasn't found!");
        }

        return movieDto;
    }
}
