using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    public class CurrentTimeIndicator : FrameworkElement
    {
        private readonly GanttParameters _parameters;
        private readonly Pen currentTimePen;
        private readonly SolidColorBrush currentTimeBrush;

        public CurrentTimeIndicator(GanttParameters parameters)
        {
            currentTimeBrush = new SolidColorBrush(Colors.Red);
            currentTimeBrush.Freeze();

            currentTimePen = new Pen(currentTimeBrush, 1.0)
            {
                DashStyle = new DashStyle(new double[] { 6, 3, 1, 3 }, 0)
            };
            currentTimePen.Freeze();

            _parameters = parameters;
        }

        protected override void OnRender(DrawingContext dc)
        {
            var currentDate = DateTime.Now;
            if (currentDate.IsGreaterThanOrEqualTo(_parameters.StartDate)
                && currentDate.IsLessThanOrEqualTo(_parameters.EndDate))
            {
                var dateUnit = _parameters.GetDateUnit(currentDate);
                var originX = _parameters.DateUnits.ToList()
                        .IndexOf(dateUnit) * _parameters.Header.UnitWidth;

                originX += (currentDate.Hour / 24.0f) * _parameters.Header.UnitWidth;
                dc.DrawLine(currentTimePen, new Point(originX, 0), new Point(originX, ActualHeight));
                dc.DrawEllipse(currentTimeBrush, null, new Point(originX, 2.5), 2.5, 2.5);
            }
        }
    }
}
