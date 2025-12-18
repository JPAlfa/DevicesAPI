using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Domain
{
    public static class DeviceStateNormalization
    {
        public static string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var s = input.Trim().ToLowerInvariant();

            s = s.Replace("_", "-").Replace(" ", "-");

            if (s == "inuse") s = "in-use";

            if (s == "unavailable") s = "inactive";

            return s;
        }
    }

}
