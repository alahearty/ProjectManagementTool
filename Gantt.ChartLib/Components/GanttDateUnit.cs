using System;

namespace Gantt.ChartLib.Components
{
    public class GanttDateUnit
    {
        public string WkDay { get; }
        public string MonthOfTheYear { get; }
        public string MonthYear { get; }
        public DayOfWeek DayOfWeek { get; }
        public string Date { get; }
        public int Day { get; }
        public int Month { get; }
        public int Year { get; }
        public int Index { get; set; }
        public bool IsSaturday => DayOfWeek == DayOfWeek.Saturday;
        public bool IsSunday => DayOfWeek == DayOfWeek.Sunday;
        public bool IsStartOfWorkDay => DayOfWeek == DayOfWeek.Monday;

        public GanttDateUnit(DateTime date)
        {
            DayOfWeek = date.DayOfWeek;
            Day = date.Day;
            Month = date.Month;
            Year = date.Year;
            Date = date.ToString("dd MMM, yy");
            MonthYear = date.ToString("MMMM, yyyy");
            MonthOfTheYear = date.ToString("MMMM");
            WkDay = GetDayAlias(date);
        }

        public DateTime GetDateTime()
        {
            return new DateTime(Year, Month, Day);
        }

        private string GetDayAlias(DateTime date)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date), "Argument cannot be null.");

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "S";

                case DayOfWeek.Monday:
                    return "M";

                case DayOfWeek.Tuesday:
                    return "T";

                case DayOfWeek.Wednesday:
                    return "W";

                case DayOfWeek.Thursday:
                    return "T";

                case DayOfWeek.Friday:
                    return "F";

                case DayOfWeek.Saturday:
                    return "S";

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
