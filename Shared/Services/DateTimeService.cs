using Shared.Services.Interfaces;

namespace Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        private static readonly TimeZoneInfo timeZone = InitTimezone();

        static TimeZoneInfo InitTimezone()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
        }

        public DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
    }
}
