using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;

namespace Devices.Tests.Application.Fakes;

public static class DeviceSeed
{
    public static Device CreateDevice(
        string name = "Device A",
        string brand = "Brand A",
        DeviceState? state = null)
    {
        state ??= DeviceState.Available;

        return new Device(name, brand, state);
    }
}
