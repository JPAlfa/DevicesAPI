using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.PatchDevice
{
    public sealed record PatchDeviceCommand(
        Guid Id,
        string? Name,
        string? Brand,
        string? State
    );
}
