using Gantt.ChartLib.Components;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gantt.ChartLib.Utils
{
    public class ProjectTimeRegion
    {
        private readonly GanttParameters parameters;
        private readonly Pen regionPen;
        private readonly SolidColorBrush regionFill;
        private readonly Pen currentTimePen;
        private readonly SolidColorBrush currentTimeBrush;

        public ProjectTimeRegion(GanttParameters parameters)
        {
            this.parameters = parameters;
            regionFill = new SolidColorBrush(Colors.Gray) { Opacity = 0.3 };
            regionFill.Freeze();

            regionPen = new Pen(
                new SolidColorBrush(Colors.Black), 1.0)
            {
                DashStyle = new DashStyle(new double[] { 6, 2, 4, 6 }, 0)
            };
            regionPen.Freeze();

            currentTimeBrush = new SolidColorBrush(Colors.Red);
            currentTimeBrush.Freeze();

            currentTimePen = new Pen(currentTimeBrush, 2.0)
            {
                DashStyle = new DashStyle(new double[] { 6, 3, 1, 3 }, 0)
            };
            regionPen.Freeze();
        }

        public void DrawRegionBoundaries(DrawingContext dc, double width, double height)
        {
            if (parameters.StartDate.IsEqualTo(parameters.EndDate))
                return;

            var start = parameters.GetDateUnit(parameters.StartDate);
            var end = parameters.GetDateUnit(parameters.EndDate);

            var units = parameters.DateUnits.ToList();
            var originX = units.IndexOf(start) * parameters.Header.UnitWidth;
            var span = (1 + units.IndexOf(end)) * parameters.Header.UnitWidth - originX;

            dc.DrawRectangle(regionFill, null, new Rect(0, 0, originX, height));
            dc.DrawRectangle(regionFill, null, new Rect(originX + span, 0, width, height));
            dc.DrawLine(regionPen, new Point(originX, 0), new Point(originX, height));
            dc.DrawLine(regionPen, new Point(originX + span, 0), new Point(originX + span, height));
        }

        public void DrawCurrentTimeIndicator(DrawingContext dc, double height)
        {
            var currentDate = DateTime.Now;
            if (currentDate.IsGreaterThanOrEqualTo(parameters.StartDate) && currentDate.IsLessThanOrEqualTo(parameters.EndDate))
            {
                var dateUnit = parameters.GetDateUnit(currentDate);
                var originX = parameters.DateUnits.ToList().IndexOf(dateUnit) * parameters.Header.UnitWidth;

                originX += (currentDate.Hour / 24.0f) * parameters.Header.UnitWidth;
                dc.DrawLine(currentTimePen, new Point(originX, 0), new Point(originX, height));
                dc.DrawEllipse(currentTimeBrush, null, new Point(originX, 4), 4, 4);
            }
        }
    }
}
