namespace TicketingSystem.Core.Extentions
{
    public static class StringExtentions
    {
        public static string RemoveWhiteSpace(this string str) {
            return str.Replace(" ", "");
        }

        public static string Reverse(this string str)
        {
            var charStr = str.ToCharArray();
            Array.Reverse(charStr);
            return new string(charStr);
        }
    }
}
