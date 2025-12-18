using Devices.Application.UseCases.DeviceUseCase.ListDevices;
using Devices.Domain;
using Devices.Domain.ValueObjects;
using Devices.Tests.Application.Fakes;
using Xunit;

namespace Devices.Application.Tests.UseCases;

public sealed class ListDevicesUseCaseTests
{
    [Fact]
    public async Task List_should_filter_by_state_with_variations()
    {
        var repo = new InMemoryDeviceRepository();
        repo.Seed(DeviceSeed.CreateDevice(name: "A", brand: "HP", state: DeviceState.Available));
        repo.Seed(DeviceSeed.CreateDevice(name: "B", brand: "HP", state: DeviceState.InUse));
        repo.Seed(DeviceSeed.CreateDevice(name: "C", brand: "Cisco", state: DeviceState.Inactive));

        var useCase = new ListDevicesUseCase(repo);

        var result = await useCase.HandleAsync(new ListDevicesQuery(null, "In Use"), CancellationToken.None);

        Assert.Single(result);
        Assert.Equal("in-use", result[0].State);
    }

    [Fact]
    public async Task List_should_fail_for_invalid_state()
    {
        var repo = new InMemoryDeviceRepository();
        var useCase = new ListDevicesUseCase(repo);

        await Assert.ThrowsAsync<DomainException>(() =>
            useCase.HandleAsync(new ListDevicesQuery(null, "printer"), CancellationToken.None));
    }
}
