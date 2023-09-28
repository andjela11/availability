using Application.Contracts;
using Application.Exceptions;
using Application.Features.Queries.GetMovie;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests;

public class GetMovieQueryHandlerTests
{
    private GetMovieQueryHandler _systemUnderTest;
    private Mock<IMovieHttpClient> _movieHttpClient;

    [SetUp]
    public void Setup()
    {
        _movieHttpClient = new Mock<IMovieHttpClient>();
    }

    [Test]
    public async Task Handle_FindByMovieId_ShouldReturnMovieDtoAsync()
    {
        // Arrange
        var movieId = 6;
        var movieDto = new MovieDto(movieId, "Title", new List<string>(), null, null);
        _movieHttpClient.Setup(x => x.GetMovieAsync(6)).Returns(Task.FromResult(movieDto));
        _systemUnderTest = new GetMovieQueryHandler(_movieHttpClient.Object);

        // Act
        var result = await _systemUnderTest.Handle(new GetMovieQuery(movieId), new CancellationToken());

        // Assert
        result.Should().NotBeNull();
        result?.Id.Should().Be(6);
    }

    [Test]
    public async Task Handle_FindByNonExistingMovieId_ShouldReturnEntityNotFoundException()
    {
        // Arrange
        var movieId = 4;
        MovieDto movieDto = default;
        _movieHttpClient.Setup(x => x.GetMovieAsync(movieId)).Returns(Task.FromResult(movieDto));
        _systemUnderTest = new GetMovieQueryHandler(_movieHttpClient.Object);

        // Act & Assert
        await _systemUnderTest.Invoking(x => x.Handle(new GetMovieQuery(movieId), new CancellationToken()))
           .Should().ThrowAsync<EntityNotFoundException>();
    }
}
