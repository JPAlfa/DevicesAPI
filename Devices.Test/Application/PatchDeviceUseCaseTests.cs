using Devices.Application.UseCases.DeviceUseCase.PatchDevice;
using Devices.Domain;
using Devices.Domain.ValueObjects;
using Devices.Tests.Application.Fakes;
using Xunit;

namespace Devices.Application.Tests.UseCases;

public sealed class PatchDeviceUseCaseTests
{
    [Fact]
    public async Task Patch_should_allow_state_change_from_in_use()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(name: "X", brand: "Y", state: DeviceState.InUse);
        repo.Seed(device);

        var useCase = new PatchDeviceUseCase(repo);

        var cmd = new PatchDeviceCommand(
            Id: device.Id,
            Name: null,
            Brand: null,
            State: "available"
        );

        var dto = await useCase.HandleAsync(cmd, CancellationToken.None);

        Assert.Equal("available", dto.State);
    }

    [Fact]
    public async Task Patch_should_fail_when_current_is_in_use_and_brand_changes()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(name: "X", brand: "Y", state: DeviceState.InUse);
        repo.Seed(device);

        var useCase = new PatchDeviceUseCase(repo);

        var cmd = new PatchDeviceCommand(
            Id: device.Id,
            Name: null,
            Brand: "NewBrand",
            State: null
        );

        await Assert.ThrowsAsync<DomainException>(() => useCase.HandleAsync(cmd, CancellationToken.None));
    }
}
