using Devices.Application.UseCases.DeviceUseCase.UpdateDevice;
using Devices.Domain;
using Devices.Domain.ValueObjects;
using Devices.Tests.Application.Fakes;
using Xunit;

namespace Devices.Application.Tests.UseCases;

public sealed class UpdateDeviceUseCaseTests
{
    [Fact]
    public async Task Update_should_update_name_brand_when_not_in_use()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(name: "Old", brand: "OldBrand", state: DeviceState.Available);
        repo.Seed(device);

        var useCase = new UpdateDeviceUseCase(repo);

        var cmd = new UpdateDeviceCommand(
            Id: device.Id,
            Name: "New",
            Brand: "NewBrand",
            State: "inactive"
        );

        var dto = await useCase.HandleAsync(cmd, CancellationToken.None);

        Assert.Equal(device.Id, dto.Id);
        Assert.Equal("New", dto.Name);
        Assert.Equal("NewBrand", dto.Brand);
        Assert.Equal("inactive", dto.State);
    }

    [Fact]
    public async Task Update_should_fail_when_current_is_in_use_and_name_changes()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(name: "Locked", brand: "LockedBrand", state: DeviceState.InUse);
        repo.Seed(device);

        var useCase = new UpdateDeviceUseCase(repo);

        var cmd = new UpdateDeviceCommand(
            Id: device.Id,
            Name: "ShouldFail",
            Brand: "LockedBrand",
            State: "available"
        );

        await Assert.ThrowsAsync<DomainException>(() => useCase.HandleAsync(cmd, CancellationToken.None));
    }
}
