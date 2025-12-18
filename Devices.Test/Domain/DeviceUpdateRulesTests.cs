using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Devices.Tests.Domain;

public sealed class DeviceUpdateRulesTests
{
    [Fact]
    public void Update_should_allow_state_change_even_if_current_is_in_use()
    {
        var device = CreateDevice(state: DeviceState.InUse);

        device.Update(name: null, brand: null, state: DeviceState.Available);

        device.State.Should().Be(DeviceState.Available);
    }

    [Fact]
    public void Update_should_not_allow_name_change_when_current_is_in_use()
    {
        var device = CreateDevice(state: DeviceState.InUse);

        var act = () => device.Update(name: "NewName", brand: null, state: null);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Update_should_not_allow_brand_change_when_current_is_in_use()
    {
        var device = CreateDevice(state: DeviceState.InUse);

        var act = () => device.Update(name: null, brand: "NewBrand", state: null);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void Update_should_allow_name_brand_change_when_current_is_available()
    {
        var device = CreateDevice(state: DeviceState.Available);

        device.Update(name: "NewName", brand: "NewBrand", state: null);

        device.Name.Should().Be("NewName");
        device.Brand.Should().Be("NewBrand");
    }

    private static Device CreateDevice(DeviceState state)
    {
        return new Device("Device1", "Brand1", state);
    }
}
