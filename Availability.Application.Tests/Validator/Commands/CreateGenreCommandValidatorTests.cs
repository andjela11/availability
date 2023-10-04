using Application.Features.Commands.CreateGenre;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Commands;

public class CreateGenreCommandValidatorTests
{
    private CreateGenreCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new CreateGenreCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var createGenreCommand = GetValidPayload();
        
        // Act
        var result = _validator.Validate(createGenreCommand);
        
        // Assert
        result.IsValid.Should().BeTrue();
    } 
    
    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var createGenreCommand = GetValidPayload() with {GenreName = string.Empty};
        
        // Act
        var result = _validator.Validate(createGenreCommand);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }

    private CreateGenreCommand GetValidPayload()
    {
        return new CreateGenreCommand("Action");
    }
}
