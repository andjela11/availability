﻿using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using Application.Features.Queries.GetReservation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.Endpoints;

public static class ReservationEndpoints
{
    public static async Task<IResult> CreateReservationAsync(
        [FromBody] CreateReservationDto createReservationDto,
        [FromServices] IMediator mediator)
    {
        var createReservationCommand = new CreateReservationCommand(createReservationDto);
        var reservationId = await mediator.Send(createReservationCommand);
        return Results.Ok(reservationId);
    }

    public static async Task<IResult> GetReservationAsync(int id,
        [FromServices] IMediator mediator)
    {
        var getReservationQuery = new GetReservationQuery(id);
        var reservationDto = await mediator.Send(getReservationQuery);
        return Results.Ok(reservationDto);
    }
}
