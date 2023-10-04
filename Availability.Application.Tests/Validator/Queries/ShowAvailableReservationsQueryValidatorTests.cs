using Application.Features.Queries.ShowAvailableReservations;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Queries;

public class ShowAvailableReservationsQueryValidatorTests
{
    private ShowAvailableReservationsQueryValidator _validator;

    [SetUp]
    public void Setup() => _validator = new ShowAvailableReservationsQueryValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        // Arrange
        var showAvailableReservationsQuery = GetValidPayload();

        // Act
        var result = _validator.Validate(showAvailableReservationsQuery);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        // Arrange
        var showAvailableReservationsQuery = GetValidPayload() with { PageNumber = -2, PageSize = -10 };

        // Act
        var result = _validator.Validate(showAvailableReservationsQuery);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private ShowAvailableReservationsQuery GetValidPayload()
    {
        return new ShowAvailableReservationsQuery(1, 10);
    }
}
