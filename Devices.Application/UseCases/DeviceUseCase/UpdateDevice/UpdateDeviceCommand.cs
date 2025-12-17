using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.UpdateDevice
{
    public sealed record UpdateDeviceCommand(
        Guid Id,
        string Name,
        string Brand,
        string State
    );
}
