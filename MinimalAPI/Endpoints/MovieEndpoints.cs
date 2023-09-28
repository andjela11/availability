using Application.Features.Queries.GetMovie;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Endpoints;

public static class MovieEndpoints
{
    public static async Task<IResult> GetMovie(int id, [FromServices] IMovieHttpClient client, [FromServices] IMediator mediator)
    {
        var getMovieQuery = new GetMovieQuery(id);
        var movieDto = await mediator.Send(getMovieQuery, new CancellationToken());
        return Results.Ok(movieDto);
    }
}