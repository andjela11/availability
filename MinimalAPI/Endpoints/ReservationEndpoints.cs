using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using Application.Features.Queries.GetAllReservations;
using Application.Features.Queries.GetReservation;
using Application.Features.Queries.ShowAvailableReservations;
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

    public static async Task<IResult> GetAllReservations([FromServices] IMediator mediator)
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservationsDto = await mediator.Send(getAllReservationsQuery);
        return Results.Ok(reservationsDto);
    }

    public static async Task<IResult> ShowAvailableReservations(
        [FromServices] IMediator mediator,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var showAvailableReservationsQuery = new ShowAvailableReservationsQuery(pageNumber, pageSize);
        var availableReservationsDto = await mediator.Send(showAvailableReservationsQuery);
        return Results.Ok(availableReservationsDto);
    }
}
