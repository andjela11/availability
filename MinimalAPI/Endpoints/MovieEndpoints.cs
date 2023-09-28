using Application.Features.Queries.GetAllMovies;
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
    
    public static async Task<IResult> GetAllMoviesAsync([FromServices] IMovieHttpClient client, [FromServices] IMediator mediator)
    {
        var getAllMoviesQuery = new GetAllMoviesQuery();
        var movieDtos = await mediator.Send(getAllMoviesQuery, new CancellationToken());
        return Results.Ok(movieDtos);
    }
}
