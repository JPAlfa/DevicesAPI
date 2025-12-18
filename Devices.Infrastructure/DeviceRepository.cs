using Devices.Application.Abstractions;
using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;
using Devices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Infrastructure
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DevicesDbContext _db;

        public DeviceRepository(DevicesDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Device device, CancellationToken ct)
        {
            await _db.Devices.AddAsync(device, ct);
        }

        public async Task<Device?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _db.Devices
                .FirstOrDefaultAsync(d => d.Id == id, ct);
        }

        public async Task<IReadOnlyList<Device>> ListAsync(string? brand, string? state, CancellationToken ct)
        {
            IQueryable<Device> query = _db.Devices.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                var b = brand.Trim();
                query = query.Where(d => d.Brand == b);
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                var s = state.Trim().ToLowerInvariant();
                var parsed = DeviceState.From(s);
                query = query.Where(d => d.State == parsed);
            }

            return await query
                .OrderByDescending(d => d.CreationTime)
                .ToListAsync(ct);
        }

        public Task Remove(Device device)
        {
            _db.Devices.Remove(device);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken ct)
        {
            await _db.SaveChangesAsync(ct);
        }
    }

}
