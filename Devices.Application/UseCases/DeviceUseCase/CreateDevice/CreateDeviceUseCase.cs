using Devices.Application.Abstractions;
using Devices.Application.Common;
using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.CreateDevice
{
    public sealed class CreateDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public CreateDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceDto> HandleAsync(CreateDeviceCommand cmd, CancellationToken ct)
        {
            var state = DeviceState.From(cmd.State);

            var device = new Domain.Entities.Device(
                name: cmd.Name,
                brand: cmd.Brand,
                state: state
            );

            await _repo.AddAsync(device, ct);
            await _repo.SaveChangesAsync(ct);

            return new DeviceDto(
                device.Id,
                device.Name,
                device.Brand ?? "",
                device.State.Value,
                device.CreationTime
            );
        }
    }
}
