using Application.Contracts;
using Application.Features.Commands.CreateReservation;
using FluentAssertions;
using NUnit.Framework;

namespace Availability.Application.Tests.Validator.Commands;

public class CreateReservationCommandValidatorTests
{
    private CreateReservationCommandValidator _validator;

    [SetUp]
    public void Setup() => _validator = new CreateReservationCommandValidator();

    [Test]
    public void Validator_ValidPayload_ShouldBeValid()
    {
        var createReservationCommand = GetValidPayload();

        var result = _validator.Validate(createReservationCommand);

        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validator_InvalidPayload_ShouldBeInvalid()
    {
        var createReservationCommand =
            GetValidPayload() with { CreateReservationDto = new CreateReservationDto(-3, 0) };

        var result = _validator.Validate(createReservationCommand);

        result.IsValid.Should().BeFalse();
    }

    private CreateReservationCommand GetValidPayload()
    {
        return new CreateReservationCommand(new CreateReservationDto(3, 100));
    }
}
