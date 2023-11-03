using Application.Contracts;
using Application.Exceptions;
using Application.Features.Commands.CancelReservation;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features.Commands;

public class CancelReservationCommandHandlerTests
{
    private Mock<IReservationHttpClient> _reservationHttpClient;
    private CancelReservationCommandHandler _sut;

    [SetUp]
    public void Setup() => _reservationHttpClient = new Mock<IReservationHttpClient>();

    [Test]
    public async Task Handle_ValidData_ShouldCancelReservation()
    {
        // Arrange
        var numberOfSeats = 5;
        var movieId = 1;
        var reservation = new ReservationDto(1, movieId, 200);
        var expected = new ReservationDto(1, movieId, 205);

        _reservationHttpClient.Setup(x => x.GetReservationAsync(movieId))
            .Returns(Task.FromResult(reservation)!);

        _sut = new CancelReservationCommandHandler(_reservationHttpClient.Object);

        // Act
        await _sut.Handle(new CancelReservationCommand(movieId, numberOfSeats), new CancellationToken());

        // Assert
        _reservationHttpClient.Verify(p => p.UpdateReservationAsync(new ReservationDto(1,movieId, expected.AvailableSeats)), Times.Once);
    }
    
    [Test]
    public void Handle_ReservationIsNull_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var movieId = 1;
        ReservationDto reservation = default;

        _reservationHttpClient.Setup(x => x.GetReservationAsync(movieId))
            .Returns(Task.FromResult(reservation)!);

        _sut = new CancelReservationCommandHandler(_reservationHttpClient.Object);

        // Act & Assert
        _sut.Invoking(async x => await x.Handle(new CancelReservationCommand(movieId, 5), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}
