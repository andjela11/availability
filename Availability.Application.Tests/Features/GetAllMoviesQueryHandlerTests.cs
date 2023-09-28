using Application.Contracts;
using Application.Features.Queries.GetAllMovies;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Availability.Application.Tests.Features;

public class GetAllMoviesQueryHandlerTests
{
    private GetAllMoviesQueryHandler _sut;
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
            x.GetAllMoviesAsync())
            .Returns(Task.FromResult(movieDtos));
        _sut = new GetAllMoviesQueryHandler(_mockHttpClient.Object);

        // Act
        var result = await _sut.Handle(new GetAllMoviesQuery(), new CancellationToken());
        
        // Assert
        result.Count.Should().Be(movieDtos.Count);
    }
    
    [Test]
    public async Task Handle_GetEmptyMoviesList_ShouldthrowException()
    {
        // Arrange
        List<MovieDto> movieDtos = default;

        _mockHttpClient.Setup(x => 
            x.GetAllMoviesAsync())
            .Returns(Task.FromResult(movieDtos));
        _sut = new GetAllMoviesQueryHandler(_mockHttpClient.Object);

        // Act & Assert
        await _sut.Invoking(x => x.Handle(new GetAllMoviesQuery(), new CancellationToken()))
            .Should().ThrowAsync<Exception>()
            .WithMessage("No movies");
    }
}
