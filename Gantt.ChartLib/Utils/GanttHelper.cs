using Gantt.ChartLib.Components;
using System;
using System.Collections.Generic;

namespace Gantt.ChartLib.Utils
{
    public class GanttHelper
    {
        public static IList<GanttDateUnit> GetDateUnits(DateTime start, DateTime end)
        {
            var date = new DateTime(start.Year, start.Month, 1);
            end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));

            var units = new List<GanttDateUnit>();
            while (date <= end)
            {
                units.Add(new GanttDateUnit(date));
                date = date.AddDays(1);
            }
            return units;
        }
    }
}
