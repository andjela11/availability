using Application.Features.Queries.FilterMovies;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Queries;

public class FilterMoviesQueryValidatorTests
{
    private FilterMoviesQueryValidator _validator;

    [SetUp]
    public void Setup() => _validator = new FilterMoviesQueryValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var filterMoviesQuery = GetValidPayload();
        
        // Act 
        var result = _validator.Validate(filterMoviesQuery);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public void Validator_InvalidPageSize_ShouldBeInvalid()
    {
        // Arrange
        var filterMoviesQuery = GetValidPayload() with { PageSize = 0};
        
        // Act 
        var result = _validator.Validate(filterMoviesQuery);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public void Validator_InvalidPageNumber_ShouldBeInvalid()
    {
        // Arrange
        var filterMoviesQuery = GetValidPayload() with { PageNumber = -1 };
        
        // Act 
        var result = _validator.Validate(filterMoviesQuery);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    private FilterMoviesQuery GetValidPayload()
    {
        return new FilterMoviesQuery(1, 10);
    }
}
