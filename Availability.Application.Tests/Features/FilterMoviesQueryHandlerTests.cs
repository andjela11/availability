using Application.Contracts;
using Application.Exceptions;
using Application.Features.Queries.FilterMovies;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features;

public class FilterMoviesQueryHandlerTests
{
    private FilterMoviesQueryHandler _sut;
    private Mock<IMovieHttpClient> _mockHttpClient;

    [SetUp]
    public void Setup() => _mockHttpClient = new Mock<IMovieHttpClient>();

    [Test]
    public async Task Handle_GetAllMovies_ShouldReturnListOfMovieDtosAsync()
    {
        // Arrange
        var movieDtos = new List<MovieDto>
        {
            new MovieDto(2, "Thor", new List<string>(), "", 2020),
            new MovieDto(3, "Thor 2", new List<string>(), "", 2020),
        };

        _mockHttpClient.Setup(x =>
            x.FilterMoviesAsync())
            .Returns(Task.FromResult(movieDtos));
        _sut = new FilterMoviesQueryHandler(_mockHttpClient.Object);

        // Act
        var result = await _sut.Handle(new FilterMoviesQuery(10,1), new CancellationToken());

        // Assert
        result.Count.Should().Be(movieDtos.Count);
    }

    [Test]
    public async Task Handle_GetEmptyMoviesList_ShouldthrowException()
    {
        // Arrange
        List<MovieDto> movieDtos = default;

        _mockHttpClient.Setup(x =>
            x.FilterMoviesAsync())
            .Returns(Task.FromResult(movieDtos));
        _sut = new FilterMoviesQueryHandler(_mockHttpClient.Object);

        // Act & Assert
        await _sut.Invoking(x => x.Handle(new FilterMoviesQuery(10,1), new CancellationToken()))
            .Should().ThrowAsync<EntityNotFoundException>()
            .WithMessage("No movies");
    }
}
