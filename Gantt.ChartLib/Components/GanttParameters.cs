using Gantt.ChartLib.Utils;
using Gantt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Gantt.ChartLib.Components
{
    public class GanttParameters : GanttItem, IChangeTracking
    {
        public IEnumerable<GanttDateUnit> DateUnits { get; private set; }
        public double TotalWidth => Header.UnitWidth * DateUnits.Count();
        public GanttHeader Header { get; }
        public GanttGrid Grid { get; }
        public Font TextFont { get; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public GanttParameters()
        {
            Header = new GanttHeader();
            Grid = new GanttGrid();
            DateUnits = new List<GanttDateUnit>();
            TextFont = Font.Weighted(Colors.Black);
        }

        public void ComputeTimeUnits(DateTime start, DateTime end)
        {
            StartDate = start;
            EndDate = end;
            DateUnits = GanttHelper.GetDateUnits(start, end);
        }

        public IEnumerable<GanttDateUnit> GetMonthlyUnits()
        {
            var units = new List<GanttDateUnit>();
            var previous = DateUnits.FirstOrDefault();
            foreach (var dateUnit in DateUnits)
            {
                if (previous.MonthYear != dateUnit.MonthYear)
                    units.Add(previous);
                previous = dateUnit;
            }
            if (previous == null)
                return units;
            units.Add(previous);
            return units;
        }

        public GanttDateUnit GetDateUnit(DateTime dateTime)
        {
            var dateUnit = DateUnits.FirstOrDefault(x =>
                x.Year == dateTime.Year && x.Month == dateTime.Month && x.Day == dateTime.Day);

            if (dateUnit == null && DateUnits.Any())
            {
                var startDate = DateUnits.First();
                if (startDate.IsGreaterThan(dateTime))
                    return startDate;

                var endDate = DateUnits.Last();
                if (endDate.IsLessThan(dateTime))
                    return endDate;
            }
            return dateUnit;
        }

        public DateTime GetDateUnit(GanttDateUnit dateUnit)
        {
            return new DateTime(dateUnit.Year, dateUnit.Month, dateUnit.Day);
        }

        public void OnChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
