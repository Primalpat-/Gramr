namespace Gramr.Logic.Extensions
{
    public static class UnixDateTimeExtension
    {
        public static DateTime ToDateTime(this long unixMilliseconds)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixMilliseconds);
        }

        public static long ToUnixMs(this DateTime date)
        {
            return ((DateTimeOffset)date).ToUnixTimeMilliseconds();
        }
    }
}
