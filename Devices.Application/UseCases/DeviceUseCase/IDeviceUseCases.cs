using Devices.Application.Common;
using Devices.Application.UseCases.DeviceUseCase.CreateDevice;
using Devices.Application.UseCases.DeviceUseCase.DeleteDevice;
using Devices.Application.UseCases.DeviceUseCase.GetDeviceById;
using Devices.Application.UseCases.DeviceUseCase.ListDevices;
using Devices.Application.UseCases.DeviceUseCase.PatchDevice;
using Devices.Application.UseCases.DeviceUseCase.UpdateDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase
{
    public interface IDeviceUseCases
    {
        Task<DeviceDto> CreateAsync(CreateDeviceCommand cmd, CancellationToken ct);
        Task<DeviceDto?> GetByIdAsync(GetDeviceByIdQuery query, CancellationToken ct);
        Task<IReadOnlyList<DeviceDto>> ListAsync(ListDevicesQuery query, CancellationToken ct);
        Task<DeviceDto> UpdateAsync(UpdateDeviceCommand cmd, CancellationToken ct);
        Task<DeviceDto> PatchAsync(PatchDeviceCommand cmd, CancellationToken ct);
        Task DeleteAsync(DeleteDeviceCommand cmd, CancellationToken ct);
    }
}
