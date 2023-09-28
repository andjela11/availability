using Application.Features.Queries.GetMovie;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests;

public class GetMovieQueryValidatorTests
{
    private GetMovieQueryValidator _validator;

    [SetUp]
    public void Setup() => _validator = new GetMovieQueryValidator();

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
        var getMovieQuery = GetValidPayload() with { Id = -2 };

        // Act
        var result = _validator.Validate(getMovieQuery);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private GetMovieQuery GetValidPayload()
    {
        return new GetMovieQuery(2);
    }

}
