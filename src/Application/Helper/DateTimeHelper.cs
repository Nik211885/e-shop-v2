using System.Globalization;

namespace Application.Helper
{
    public class DateTimeHelper
    {
        public static DateTime ConvertStringToDateTimeFormat(string dateTime)
        {
            var isDate = DateTime.TryParseExact(dateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
            return isDate ? result : throw new ArgumentException("Invalid date format dd/MM/yyyy");
        }

        public static DateTimeOffset ConvertUtcToUtf7(DateTimeOffset dateTime)
            => dateTime.AddHours(7);
    }
}