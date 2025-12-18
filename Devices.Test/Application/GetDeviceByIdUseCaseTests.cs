using Devices.Application.UseCases.DeviceUseCase.GetDeviceById;
using Devices.Tests.Application.Fakes;
using Xunit;
using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;

namespace Devices.Application.Tests.UseCases;

public sealed class GetDeviceByIdUseCaseTests
{
    [Fact]
    public async Task GetById_should_return_device_when_exists()
    {
        var repo = new InMemoryDeviceRepository();
        var device = DeviceSeed.CreateDevice(name: "Scanner X", brand: "Canon");
        repo.Seed(device);

        var useCase = new GetDeviceByIdUseCase(repo);

        var dto = await useCase.HandleAsync(new GetDeviceByIdQuery(device.Id), CancellationToken.None);

        Assert.NotNull(dto);
        Assert.Equal(device.Id, dto.Id);
        Assert.Equal("Scanner X", dto.Name);
        Assert.Equal("Canon", dto.Brand);
    }

    [Fact]
    public async Task GetById_should_return_null_when_not_found()
    {
        var repo = new InMemoryDeviceRepository();
        var useCase = new GetDeviceByIdUseCase(repo);

        var dto = await useCase.HandleAsync(new GetDeviceByIdQuery(Guid.NewGuid()), CancellationToken.None);

        Assert.Null(dto);
    }
}
