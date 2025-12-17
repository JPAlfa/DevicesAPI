using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Devices.API.Contracts
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceStateDto
    {
        [EnumMember(Value = "available")]
        Available,

        [EnumMember(Value = "in-use")]
        InUse,

        [EnumMember(Value = "inactive")]
        Inactive
    }
}
