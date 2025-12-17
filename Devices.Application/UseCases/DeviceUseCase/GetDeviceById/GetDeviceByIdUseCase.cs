using Devices.Application.Abstractions;
using Devices.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.GetDeviceById
{
    public sealed class GetDeviceByIdUseCase
    {
        private readonly IDeviceRepository _repo;

        public GetDeviceByIdUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<DeviceDto?> HandleAsync(GetDeviceByIdQuery query, CancellationToken ct)
        {
            var device = await _repo.GetByIdAsync(query.Id, ct);
            if (device is null) return null;

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
