using Application.Contracts;
using Application.Exceptions;
using Application.Features.Queries.GetReservation;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features.Queries;

public class GetReservationQueryHandlerTests
{
    private GetReservationQueryHandler _sut;
    private Mock<IReservationHttpClient> _reservationHttpClient;

    [SetUp]
    public void Setup() => _reservationHttpClient = new Mock<IReservationHttpClient>();

    [Test]
    public async Task Handle_GetReservationWithValidId_ShouldGetReservationAsync()
    {
        // Arrange
        var reservationId = 3;
        var reservationDto = new ReservationDto(3, 3, 200);

        _reservationHttpClient.Setup(x => x.GetReservationAsync(reservationId))
            .Returns(Task.FromResult<ReservationDto?>(reservationDto));

        _sut = new GetReservationQueryHandler(_reservationHttpClient.Object);

        // Act
        var result = await _sut.Handle(new GetReservationQuery(reservationId), new CancellationToken());

        // Assert
        result.AvailableSeats.Should().Be(200);
        result.MovieId.Should().Be(3);
    }

    [Test]
    public async Task Handle_GetReservationWithNonExistingId_ShouldThrowEntityNotFoundExceptionAsync()
    {
        // Arrange
        var reservationId = 3;
        ReservationDto reservationDto = default;

        _reservationHttpClient.Setup(x => x.GetReservationAsync(reservationId))
            .Returns(Task.FromResult<ReservationDto?>(reservationDto));

        _sut = new GetReservationQueryHandler(_reservationHttpClient.Object);

        // Act & Assert
        await _sut.Invoking(x => x.Handle(new GetReservationQuery(reservationId), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}
