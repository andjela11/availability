using Application.Features.Commands.UpdateGenre;
using Application.Features.Queries.GetMovie;
using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator;


public class UpdateGenreCommandValidatorTests
{
    private UpdateGenreCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new UpdateGenreCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var getMovieQuery = GetValidPayload();

        // Act
        var result = _validator.Validate(getMovieQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    private UpdateGenreCommand GetValidPayload()
    {
        return new UpdateGenreCommand(new Genre()
        {
            Id = "id",
            Name = string.Empty
        });
    }
}
