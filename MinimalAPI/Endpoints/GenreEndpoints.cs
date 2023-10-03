using Application.Features.Commands.CreateGenre;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Endpoints;

public static class GenreEndpoints
{
    public static async Task<IResult> CreateGenre([FromServices] IMediator mediator,[FromQuery] string genreName)
    {
        var createGenreCommand = new CreateGenreCommand(genreName);
        var id = await mediator.Send(createGenreCommand);
        return Results.Created("", id);
    }
}
