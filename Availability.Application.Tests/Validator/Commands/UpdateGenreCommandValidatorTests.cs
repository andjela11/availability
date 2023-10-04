using Application.Features.Commands.UpdateGenre;
using Domain;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Commands;


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
    
    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var getMovieQuery = GetValidPayload() with { Genre = new Genre { Name = string.Empty }};

        // Act
        var result = _validator.Validate(getMovieQuery);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    private UpdateGenreCommand GetValidPayload()
    {
        return new UpdateGenreCommand(new Genre()
        {
            Id = "id",
            Name = "Action"
        });
    }
}
