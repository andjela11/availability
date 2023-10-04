using Application.Features.Commands.DeleteGenre;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Commands;

public class DeleteGenreCommandValidatorTests
{
    private DeleteGenreCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new DeleteGenreCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var deleteGenreCommand = GetValidPayload();
        
        // Act
        var result = _validator.Validate(deleteGenreCommand);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var deleteGenreCommand = GetValidPayload() with {GenreId = string.Empty};
        
        // Act
        var result = _validator.Validate(deleteGenreCommand);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }

    private DeleteGenreCommand GetValidPayload()
    {
        return new DeleteGenreCommand("Id");
    }
}
