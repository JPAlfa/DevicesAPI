using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.ListDevices
{
    public sealed record ListDevicesQuery(string? Brand, string? State);
}
