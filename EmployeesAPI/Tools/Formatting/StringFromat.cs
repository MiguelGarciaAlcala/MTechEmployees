using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Formatting
{
    public static class StringFormat
    {
        public static string Trim(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        public static bool IsLengthBetween(string value, int min, int max)
        {
            if (!(min <= max))
                throw new Exception("A valid range must be provided.");

            return value.Length >= min && value.Length <= max;
        }

        public static bool IsNotEmpty(string value, int max = 0, bool trim = false)
        {
            if (trim)
                value = Trim(value);

            return !string.IsNullOrEmpty(value) && (max <= 0 || value.Length <= max);
        }
    }
}
