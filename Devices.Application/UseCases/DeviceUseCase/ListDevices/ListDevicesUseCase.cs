using Devices.Application.Abstractions;
using Devices.Application.Common;
using Devices.Domain;
using Devices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.ListDevices
{
    public sealed class ListDevicesUseCase
    {
        private readonly IDeviceRepository _repo;

        public ListDevicesUseCase(IDeviceRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<DeviceDto>> HandleAsync(ListDevicesQuery query, CancellationToken ct)
        {
            var devices = await _repo.ListAsync(query.Brand, DeviceStateNormalization.Normalize(query.State), ct);

            return devices
                .Select(ToDto)
                .ToList();
        }

        private static DeviceDto ToDto(Domain.Entities.Device d) =>
            new(d.Id, d.Name, d.Brand ?? "", d.State.Value, d.CreationTime);
    }
}
