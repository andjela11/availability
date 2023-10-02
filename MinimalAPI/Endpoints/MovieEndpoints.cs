using Application.Features.Queries.FilterMovies;
using Application.Features.Queries.GetMovie;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Endpoints;

public static class MovieEndpoints
{
    public static async Task<IResult> GetMovieAsync(
        int id,
        [FromServices] IMovieHttpClient client,
        [FromServices] IMediator mediator)
    {
        var getMovieQuery = new GetMovieQuery(id);
        var movieDto = await mediator.Send(getMovieQuery, new CancellationToken());
        return Results.Ok(movieDto);
    }
    
    public static async Task<IResult> FilterMoviesAsync(
        [FromServices] IMovieHttpClient client,
        [FromServices] IMediator mediator,
        [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var filterMoviesQuery = new FilterMoviesQuery(pageSize, pageNumber);
        var movieDtos = await mediator.Send(filterMoviesQuery, new CancellationToken());
        return Results.Ok(movieDtos);
    }
}
