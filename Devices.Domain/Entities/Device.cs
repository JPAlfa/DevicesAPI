using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devices.Domain.ValueObjects;

namespace Devices.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string? Brand { get; private set; }
        public DeviceState State { get; private set; }
        public DateTime CreationTime {  get; private set; }

        public Device(string name, string brand, DeviceState state)
        {
            Id = Guid.NewGuid();
            Name = name;
            Brand = brand;
            State = state ?? throw new ArgumentNullException(nameof(state));
            CreationTime = DateTime.UtcNow;
        }

        public void Update(string? name, string? brand, string? description, DeviceState? state)
        {
            if (!State.CanUpdateNameAndBrand())
            {
                if (name is not null && !string.Equals(name, Name, StringComparison.Ordinal))
                    throw new DomainException("Device in-use: name cannot be updated.");

                if (brand is not null && !string.Equals(brand, Brand, StringComparison.Ordinal))
                    throw new DomainException("Device in-use: brand cannot be updated.");
            }

            if (name is not null) Name = name;
            if (brand is not null) Brand = brand;
            if (state is not null) State = state;
        }

        public void EnsureCanDelete()
        {
            if (!State.CanBeDeleted())
                throw new DomainException("Device in-use: cannot be deleted.");
        }
    }
}

