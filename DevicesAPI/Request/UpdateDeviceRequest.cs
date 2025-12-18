using Devices.API.Contracts;

namespace Devices.API.Request
{
    public class UpdateDeviceRequest
    {
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public required string State { get; set; }
    }
}
