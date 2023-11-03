using Application.Features.Commands.CancelReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Commands;

public class CancelReservationCommandValidatorTests
{
    private CancelReservationCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new CancelReservationCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeTrue()
    {
        // Arrange
        var cancelReservationCommandValidator = GetValidPayload();

        // Act
        var result = _validator.Validate(cancelReservationCommandValidator);

        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Test]
    public void Validator_NegativeNumberOfSeats_ShouldBeTrue()
    {
        // Arrange
        var cancelReservationCommandValidator = GetValidPayload() with{NumberOfSeats = -1};

        // Act
        var result = _validator.Validate(cancelReservationCommandValidator);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Test]
    public void Validator_NegativeMovieId_ShouldBeTrue()
    {
        // Arrange
        var cancelReservationCommandValidator = GetValidPayload() with{MovieId = -1};

        // Act
        var result = _validator.Validate(cancelReservationCommandValidator);

        // Assert
        result.IsValid.Should().BeFalse();
    }
    

    private CancelReservationCommand GetValidPayload() => new(1, 3);
}
