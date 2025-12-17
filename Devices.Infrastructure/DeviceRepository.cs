using Devices.Application.Abstractions;
using Devices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Infrastructure
{
    public class DeviceRepository : IDeviceRepository
    {
        public DeviceRepository() { }

        public async Task AddAsync(Device device, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Device?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Device>> ListAsync(string? brand, string? state, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(Device device)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

    }
}
