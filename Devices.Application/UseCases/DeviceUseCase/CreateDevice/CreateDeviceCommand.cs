using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.UseCases.DeviceUseCase.CreateDevice
{
    public sealed record CreateDeviceCommand(
        string Name,
        string Brand,
        string State
    );
}
