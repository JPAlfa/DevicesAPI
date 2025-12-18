using FluentAssertions;
using global::Devices.Domain.ValueObjects;
using Xunit;

namespace Devices.Test.Domain;

public sealed class DeviceStateTests
{
    [Theory]
    [InlineData("available", "available")]
    [InlineData("AVAILABLE", "available")]
    [InlineData(" Available ", "available")]
    [InlineData("in-use", "in-use")]
    [InlineData("In Use", "in-use")]
    [InlineData("in_use", "in-use")]
    [InlineData("INUSE", "in-use")]
    [InlineData("inactive", "inactive")]
    [InlineData("INACTIVE", "inactive")]
    public void From_should_normalize_and_parse(string input, string expected)
    {
        var state = DeviceState.From(input);

        state.Value.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("printer")]
    [InlineData("in--use")]
    public void From_should_throw_on_invalid_values(string input)
    {
        var act = () => DeviceState.From(input);

        act.Should().Throw<Exception>(); // DomainException no seu projeto
    }

    [Fact]
    public void CanBeDeleted_should_be_false_when_in_use()
    {
        DeviceState.InUse.CanBeDeleted().Should().BeFalse();
        DeviceState.Available.CanBeDeleted().Should().BeTrue();
        DeviceState.Inactive.CanBeDeleted().Should().BeTrue();
    }

    [Fact]
    public void CanUpdateNameAndBrand_should_be_false_when_in_use()
    {
        DeviceState.InUse.CanUpdateNameAndBrand().Should().BeFalse();
        DeviceState.Available.CanUpdateNameAndBrand().Should().BeTrue();
        DeviceState.Inactive.CanUpdateNameAndBrand().Should().BeTrue();
    }
}
