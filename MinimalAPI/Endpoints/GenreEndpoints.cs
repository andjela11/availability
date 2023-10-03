using Application.Features.Commands.CreateGenre;
using Application.Features.Commands.DeleteGenre;
using Application.Features.Commands.UpdateGenre;
using Application.Features.Queries.GetAllGenres;
using Application.Features.Queries.GetMovie;
using Domain;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Endpoints;

public static class GenreEndpoints
{
    public static async Task<IResult> CreateGenre(
        [FromServices] IMediator mediator,
        [FromQuery] string genreName)
    {
        var createGenreCommand = new CreateGenreCommand(genreName);
        var id = await mediator.Send(createGenreCommand);
        return Results.Created("", id);
    }

    public static async Task<IResult> GetAllGenres([FromServices] IMediator mediator)
    {
        var getAllGenresQuery = new GetAllGenresQuery();
        var genres = await mediator.Send(getAllGenresQuery);
        return Results.Ok(genres);
    }

    public static async Task<IResult> UpdateGenre(
        [FromServices] IMediator mediator, 
        [FromBody] Genre genre)
    {
        var updateGenreCommand = new UpdateGenreCommand(genre);
        // var getMovieQuery = new GetMovieQuery(5);
        // var result = await mediator.Send(getMovieQuery);
        await mediator.Send(updateGenreCommand);
        return Results.Ok();
    }
    
    public static async Task<IResult> DeleteGenre(
        [FromServices] IMediator mediator,
        [FromQuery] string genreId)
    {
        var deleteGenreCommand = new DeleteGenreCommand(genreId);
        await mediator.Send(deleteGenreCommand);
        return Results.Ok();
    }
}
