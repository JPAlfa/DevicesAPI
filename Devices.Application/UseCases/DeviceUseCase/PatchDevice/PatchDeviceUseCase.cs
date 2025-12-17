using Devices.Application.Abstractions;
using Devices.Application.Common;
using Devices.Application.Common.Exceptions;
using Devices.Domain.Entities;
using Devices.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.PatchDevice
{

    public sealed class PatchDeviceUseCase
    {
        private readonly IDeviceRepository _repo;

        public PatchDeviceUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceDto> HandleAsync(PatchDeviceCommand cmd, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(cmd.Id, ct)
                ?? throw new NotFoundException($"Device '{cmd.Id}' was not found.");

            DeviceState? newState = null;
            if (cmd.State is not null)
                newState = DeviceState.From(cmd.State);

            // Regra final no domínio
            device.Update(
                name: cmd.Name,
                brand: cmd.Brand,
                state: newState
            );

            await _repo.SaveChangesAsync(ct);

            return ToDto(device);
        }

        private static DeviceDto ToDto(Device d) =>
            new(d.Id, d.Name, d.Brand ?? "", d.State.Value, d.CreationTime);
    }
}
