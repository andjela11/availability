using Application.Contracts;
using Application.Exceptions;
using Application.Features.Commands.CreateReservation;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features;

public class CreateReservationCommandHandlerTests
{
    private CreateReservationCommandHandler _sut;
    private Mock<IMovieHttpClient> _movieHttpClient;
    private Mock<IReservationHttpClient> _reservationHttpClient;

    [SetUp]
    public void Setup()
    {
        _movieHttpClient = new Mock<IMovieHttpClient>();
        _reservationHttpClient = new Mock<IReservationHttpClient>();
    }

    [Test]
    public async Task Handle_CreateReservation_ShouldCreateReservationAsync()
    {
        // Arrange
        var movieId = 4;
        var reservationId = 2;

        var reservationDto = new CreateReservationDto(movieId, 200);
        var movieDto = new MovieDto(movieId, "Title", new List<string>(), null, null);

        _movieHttpClient.Setup(x => x.GetMovieAsync(movieId))
                        .Returns(Task.FromResult(movieDto));
        _reservationHttpClient.Setup(x => x.CreateReservationAsync(reservationDto))
                        .Returns(Task.FromResult(reservationId));

        _sut = new CreateReservationCommandHandler(_movieHttpClient.Object, _reservationHttpClient.Object);

        // Act
        var result = await _sut.Handle(new CreateReservationCommand(reservationDto), new CancellationToken());

        // Assert
        result.Should().Be(2);
    }

    [Test]
    public void Handle_CreateReservationWithNonExistingMovie_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var movieId = 4;
        var reservationId = 2;

        var reservationDto = new CreateReservationDto(movieId, 200);
        MovieDto movieDto = default;

        _movieHttpClient.Setup(x => x.GetMovieAsync(movieId))
            .Returns(Task.FromResult(movieDto));
        _reservationHttpClient.Setup(x => x.CreateReservationAsync(reservationDto))
            .Returns(Task.FromResult(reservationId));

        _sut = new CreateReservationCommandHandler(_movieHttpClient.Object, _reservationHttpClient.Object);

        // Act & Assert
        _sut.Invoking(x => x.Handle(new CreateReservationCommand(reservationDto), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
}
