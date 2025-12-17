using Devices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Abstractions
{
    public interface IDeviceRepository
    {
        Task AddAsync(Device device, CancellationToken ct);
        Task<Device?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Device>> ListAsync(string? brand, string? state, CancellationToken ct);
        Task Remove(Device device);
        Task SaveChangesAsync(CancellationToken ct);
    }
}
