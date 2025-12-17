using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Application.Common;

public sealed record DeviceDto(
    Guid Id,
    string Name,
    string Brand,
    string State,
    DateTime CreationTime
);
