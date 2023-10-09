using Application.Features.Queries.GetReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Queries;

public class GetReservationQueryValidatorTests
{
    private GetReservationQueryValidator _validator;

    [SetUp]
    public void Setup() => _validator = new GetReservationQueryValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var getReservationQuery = GetValidPayload();

        // Act
        var result = _validator.Validate(getReservationQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange 
        var getReservationQuery = GetValidPayload() with { Id = -3 };

        // Act
        var result = _validator.Validate(getReservationQuery);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private GetReservationQuery GetValidPayload() => new(6);
}
