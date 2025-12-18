using Devices.Application.Abstractions;
using Devices.Application.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.DeleteDevice
{
    public sealed class DeleteDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public DeleteDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task HandleAsync(DeleteDeviceCommand cmd, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(cmd.Id, ct)
                ?? throw new NotFoundException($"Device '{cmd.Id}' was not found.");

            device.EnsureCanDelete();

            await _repo.Remove(device);
            await _repo.SaveChangesAsync(ct);
        }
    }
}
