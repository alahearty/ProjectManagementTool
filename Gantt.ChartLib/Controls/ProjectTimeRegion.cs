using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    internal class ProjectTimeRegion
    {
        private readonly GanttParameters parameters;
        private readonly Pen regionPen;
        private readonly SolidColorBrush regionFill;

        internal ProjectTimeRegion(GanttParameters parameters)
        {
            this.parameters = parameters;
            regionFill = new SolidColorBrush(Colors.Gray) { Opacity = 0.1 };
            regionFill.Freeze();

            regionPen = new Pen(
                new SolidColorBrush(Colors.Black), 1.0)
            {
                DashStyle = new DashStyle(new double[] { 6, 2, 4, 6 }, 0)
            };
            regionPen.Freeze();
        }

        internal void DrawRegionBoundaries(DrawingContext dc, double width, double height)
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
    }
}
