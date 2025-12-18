using Devices.Application.UseCases.DeviceUseCase.DeleteDevice;
using Devices.Domain;
using Devices.Domain.ValueObjects;
using Devices.Tests.Application.Fakes;
using Xunit;

namespace Devices.Test.Application;

public sealed class DeleteDeviceUseCaseTests
{
    [Fact]
    public async Task Delete_should_remove_device_when_allowed()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(state: DeviceState.Available);
        repo.Seed(device);

        var useCase = new DeleteDeviceUseCase(repo);

        await useCase.HandleAsync(new DeleteDeviceCommand(device.Id), CancellationToken.None);

        var fetched = await repo.GetByIdAsync(device.Id, CancellationToken.None);
        Assert.Null(fetched);
    }

    [Fact]
    public async Task Delete_should_fail_when_in_use()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(state: DeviceState.InUse);
        repo.Seed(device);

        var useCase = new DeleteDeviceUseCase(repo);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.HandleAsync(new DeleteDeviceCommand(device.Id), CancellationToken.None));
    }
}
