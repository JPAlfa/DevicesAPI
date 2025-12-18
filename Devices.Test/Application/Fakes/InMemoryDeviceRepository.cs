using Devices.Application.Abstractions;
using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;

namespace Devices.Tests.Application.Fakes;

public sealed class InMemoryDeviceRepository : IDeviceRepository
{
    private readonly List<Device> _devices = new();

    public Task AddAsync(Device device, CancellationToken ct)
    {
        _devices.Add(device);
        return Task.CompletedTask;
    }

    public Task<Device?> GetByIdAsync(Guid id, CancellationToken ct)
        => Task.FromResult(_devices.FirstOrDefault(d => d.Id == id));

    public Task<IReadOnlyList<Device>> ListAsync(string? brand, string? state, CancellationToken ct)
    {
        IEnumerable<Device> q = _devices;

        if (!string.IsNullOrWhiteSpace(brand))
            q = q.Where(d => d.Brand == brand.Trim());

        if (!string.IsNullOrWhiteSpace(state))
        {
            var parsed = DeviceState.From(state);
            q = q.Where(d => d.State == parsed);
        }

        return Task.FromResult((IReadOnlyList<Device>)q.ToList());
    }

    public Task Remove(Device device)
    {
        _devices.Remove(device);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct)
        => Task.CompletedTask;

    public void Seed(Device device) => _devices.Add(device);
}
