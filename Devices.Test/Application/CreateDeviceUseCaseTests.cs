using Devices.Tests.Application.Fakes;
using Devices.Application.UseCases.DeviceUseCase.CreateDevice;
using Xunit;
using Devices.Domain;

namespace Devices.Application.Tests.UseCases;

public sealed class CreateDeviceUseCaseTests
{
    [Fact]
    public async Task Create_should_return_created_device()
    {
        var repo = new InMemoryDeviceRepository();
        var useCase = new CreateDeviceUseCase(repo);

        var cmd = new CreateDeviceCommand(
            Name: "Printer A",
            Brand: "HP",
            State: "available"
        );

        var dto = await useCase.HandleAsync(cmd, CancellationToken.None);

        Assert.NotNull(dto);
        Assert.NotEqual(Guid.Empty, dto.Id);
        Assert.Equal("Printer A", dto.Name);
        Assert.Equal("HP", dto.Brand);
        Assert.Equal("available", dto.State);
    }

    [Fact]
    public async Task Create_should_fail_for_invalid_state()
    {
        var repo = new InMemoryDeviceRepository();
        var useCase = new CreateDeviceUseCase(repo);

        var cmd = new CreateDeviceCommand(
            Name: "X",
            Brand: "Y",
            State: "printer"
        );

        await Assert.ThrowsAsync<DomainException>(() => useCase.HandleAsync(cmd, CancellationToken.None));
    }
}
