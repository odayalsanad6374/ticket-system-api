
namespace TicketingSystem.Core.Extentions
{
    public static class NumberHelper
    {
        public static bool IsBetween(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}
