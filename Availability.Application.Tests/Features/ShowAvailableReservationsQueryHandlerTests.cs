using Application.Contracts;
using Application.Exceptions;
using Application.Features.Queries.ShowAvailableReservations;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features;

public class ShowAvailableReservationsQueryHandlerTests
{
    private ShowAvailableReservationsQueryHandler _sut;
    private Mock<IMovieHttpClient> _movieHttpClient;
    private Mock<IReservationHttpClient> _reservationHttpClient;

    [SetUp]
    public void Setup()
    {
        _movieHttpClient = new Mock<IMovieHttpClient>();
        _reservationHttpClient = new Mock<IReservationHttpClient>();
    }

    [Test]
    public async Task Handle_ShowAvailableReservations_ShouldReturnAvailableReservationsAsync()
    {
        // Arrange
        var movies = new List<MovieDto>()
        {
            new(1, "Thor", null, null, 2020), new(2, "Thor 2", null, null, 2020),
        };
        
        var reservations = new List<ReservationDto>()
        {
            new(1, 100), new(2, 200),
        };

        _movieHttpClient.Setup(x => x.GetAllMoviesAsync())
            .Returns(Task.FromResult<List<MovieDto>?>(movies));
        _reservationHttpClient.Setup(x => x.GetAllReservationsAsync())
            .Returns(Task.FromResult<List<ReservationDto>?>(reservations));

        _sut = new ShowAvailableReservationsQueryHandler(_reservationHttpClient.Object, _movieHttpClient.Object);

        // Act
        var result = await _sut.Handle(new ShowAvailableReservationsQuery(), new CancellationToken());
        
        // Assert
        result.Should().BeOfType(typeof(List<AvailableReservationDto>));
        result.Count.Should().Be(2);
        result.First().Title.Should().Be(movies.First().Title);
        result.First().AvailableSeats.Should().Be(reservations.First().AvailableSeats);
    }
    
    [Test]
    public async Task Handle_MovieListEmpty_ShouldReturnEntityNotFoundExceptionAsync()
    {
        // Arrange
        List<MovieDto> movies = default;
        
        var reservations = new List<ReservationDto>()
        {
            new(1, 100), new(2, 200),
        };

        _movieHttpClient.Setup(x => x.GetAllMoviesAsync())
            .Returns(Task.FromResult<List<MovieDto>?>(movies));
        _reservationHttpClient.Setup(x => x.GetAllReservationsAsync())
            .Returns(Task.FromResult<List<ReservationDto>?>(reservations));

        _sut = new ShowAvailableReservationsQueryHandler(_reservationHttpClient.Object, _movieHttpClient.Object);

        // Act & Assert
        await _sut.Invoking(x => x.Handle(new ShowAvailableReservationsQuery(), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
    
    [Test]
    public async Task Handle_ReservationsListEmpty_ShouldReturnEntityNotFoundExceptionAsync()
    {
        // Arrange
        var movies = new List<MovieDto>()
        {
            new(1, "Thor", null, null, 2020), new(2, "Thor 2", null, null, 2020),
        };

        List<ReservationDto> reservations = default;

        _movieHttpClient.Setup(x => x.GetAllMoviesAsync())
            .Returns(Task.FromResult<List<MovieDto>?>(movies));
        _reservationHttpClient.Setup(x => x.GetAllReservationsAsync())
            .Returns(Task.FromResult<List<ReservationDto>?>(reservations));

        _sut = new ShowAvailableReservationsQueryHandler(_reservationHttpClient.Object, _movieHttpClient.Object);

        // Act & Assert
        await _sut.Invoking(x => x.Handle(new ShowAvailableReservationsQuery(), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>();
    }
    
    [Test]
    public async Task Handle_BothListsEmpty_ShouldReturnEntityNotFoundExceptionAsync()
    {
        // Arrange
        var movies = new List<MovieDto>();

        var reservations = new List<ReservationDto>();

        _movieHttpClient.Setup(x => x.GetAllMoviesAsync())
            .Returns(Task.FromResult<List<MovieDto>?>(movies));
        _reservationHttpClient.Setup(x => x.GetAllReservationsAsync())
            .Returns(Task.FromResult<List<ReservationDto>?>(reservations));

        _sut = new ShowAvailableReservationsQueryHandler(_reservationHttpClient.Object, _movieHttpClient.Object);

        // Act
        var result = await _sut.Handle(new ShowAvailableReservationsQuery(), new CancellationToken());
        
        // Assert
        result.Should().BeOfType(typeof(List<AvailableReservationDto>));
        result.Count.Should().Be(0);
    }
}
