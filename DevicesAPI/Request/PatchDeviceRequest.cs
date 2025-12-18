using Devices.API.Contracts;

namespace Devices.API.Request
{
    public class PatchDeviceRequest
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? State { get; set; }
    }
}
