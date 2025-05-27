using System.Globalization;

namespace WorldTracker.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool TryParseInvariant(this string value, out double result)
        {
           return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }
    }
}
