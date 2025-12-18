namespace Devices.API.Response
{
    public sealed class DeviceResponse
    {
        public Guid Id { get; init; }

        public string Name { get; init; } = default!;

        public string Brand { get; init; } = default!;

        public string State { get; init; } = default!;

        public DateTime CreationTime { get; init; }
    }
}
