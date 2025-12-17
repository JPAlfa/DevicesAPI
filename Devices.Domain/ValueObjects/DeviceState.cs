using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Domain.ValueObjects
{
    public sealed class DeviceState
    {
        public static readonly DeviceState Available = new("available");
        public static readonly DeviceState InUse = new("in-use");
        public static readonly DeviceState Inactive = new("inactive");

        public string Value { get; }

        private DeviceState(string value)
        {
            Value = value;
        }

        public bool CanBeDeleted()
            => this != InUse;

        public bool CanUpdateNameAndBrand()
            => this != InUse;

        public override bool Equals(object? obj)
        {
            if (obj is not DeviceState other)
                return false;

            return Value == other.Value;
        }

        public static DeviceState From(string value)
        {
            var v = value.Trim().ToLowerInvariant();
            return v switch
            {
                "available" => Available,
                "in-use" => InUse,
                "inactive" => Inactive,
                _ => throw new DomainException("Invalid device state.")
            };
        }

        public override int GetHashCode()
            => Value.GetHashCode();

        public override string ToString()
            => Value;
    }
}
