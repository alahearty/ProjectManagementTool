using Gantt.ChartLib.Components;
using System;

namespace Gantt.ChartLib.Utils
{
    public static class GanttDateExtensions 
    {
        public static bool IsGreaterThan(this DateTime dateTime, DateTime date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) < 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) > 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) < 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) <= 0)
                return false;

            return false;
        }

        public static bool IsLessThan(this DateTime dateTime, DateTime date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) > 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) < 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) > 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) >= 0)
                return false;

            return false;
        }

        public static bool IsGreaterThan(this GanttDateUnit dateTime, GanttDateUnit date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) < 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) > 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) < 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) <= 0)
                return false;

            return false;
        }

        public static bool IsLessThan(this GanttDateUnit dateTime, GanttDateUnit date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) > 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) < 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) > 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) >= 0)
                return false;

            return false;
        }

        public static bool IsGreaterThan(this GanttDateUnit dateTime, DateTime date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) < 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) > 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) < 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) > 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) <= 0)
                return false;

            return false;
        }

        public static bool IsLessThan(this GanttDateUnit dateTime, DateTime date)
        {
            if (dateTime == null)
                throw new InvalidOperationException("dateTime must be of type DateTime or GanttDateUnit");

            if (dateTime.Year.CompareTo(date.Year) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Year) > 0)
                return false;

            if (dateTime.Month.CompareTo(date.Month) < 0)
                return true;
            if (dateTime.Month.CompareTo(date.Month) > 0)
                return false;

            if (dateTime.Day.CompareTo(date.Day) < 0)
                return true;
            if (dateTime.Year.CompareTo(date.Day) >= 0)
                return false;

            return false;
        }

        public static bool IsLessThanOrEqualTo(this DateTime date, DateTime toDate)
        {
            return date.IsLessThan(toDate) || date.IsEqualTo(toDate);
        }

        public static bool IsGreaterThanOrEqualTo(this DateTime date, DateTime toDate)
        {
            return date.IsGreaterThan(toDate) || date.IsEqualTo(toDate);
        }

        public static bool IsEqualTo(this DateTime date, DateTime toTate)
        {
            if (date.Year == toTate.Year
                && date.Month == toTate.Month
                && date.Day == toTate.Day)
            {
                return true;
            }
            return false;
        }

        public static bool IsEqualTo(this GanttDateUnit date, DateTime toTate)
        {
            if (date.Year == toTate.Year
                && date.Month == toTate.Month
                && date.Day == toTate.Day)
            {
                return true;
            }
            return false;
        }
    }
}
