using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    public class GanttHeaderCanvas : Canvas
    {
        private readonly GanttParameters parameters;
        private readonly GanttHeader header;
        private readonly Font normalBlack;
        private readonly Font normalRed;
        private readonly Font weightedBlack;
        private readonly Font weightedRed;

        private Pen minorTickPen;
        private Pen majorTickPen;
        private Pen wkendTickPen;

        public GanttHeaderCanvas(GanttParameters parameters)
        {
            this.parameters = parameters;
            header = parameters.Header;

            normalRed = Font.Normal(Colors.Red);
            normalBlack = Font.Normal(Colors.Black);
            weightedRed = Font.Weighted(Colors.Red);
            weightedBlack = Font.Weighted(Colors.Black);

            InitDrawingPens();
            PropertyBindings();
        }

        private void InitDrawingPens()
        {
            var stroke = new SolidColorBrush(Colors.Red)
            {
                Opacity = 0.75
            };
            wkendTickPen = new Pen(stroke, 1.0);
            wkendTickPen.Freeze();

            var minorStroke = new BrushConverter()
                .ConvertFromString(header.MinorStroke) as Brush;
            minorStroke.Opacity = 0.25;
            minorStroke.Freeze();

            minorTickPen = new Pen(minorStroke, 1.0);
            minorTickPen.Freeze();

            var majorStroke = new BrushConverter()
                .ConvertFromString(header.MajorStroke) as Brush;
            majorStroke.Freeze();

            majorTickPen = new Pen(majorStroke, 1.0);
            majorStroke.Freeze();
        }

        private void PropertyBindings()
        {
            BindingBuilder binding =
                new BindingBuilder(this, HeightProperty, header, nameof(header.TotalHeight));
            binding.Append(BindingMode.OneWay)
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Bind();
        }

        protected override void OnRender(DrawingContext dc)
        {
            var dateUnits = parameters.DateUnits;
            int counter = 0;
            foreach (var dateUnit in dateUnits)
            {
                var originX = counter * header.UnitWidth;
                AppendDayOfWeekBlock(dc, dateUnit, originX);
                AppendDayOfWeekText(dc, dateUnit, originX);
                AppendNumberText(dc, dateUnit, originX);

                dateUnit.Index = counter;
                counter++;
            }
            AppendMonthlyText(dc);
            AddHeaderOutlines(dc);
        }

        private void AppendMonthlyText(DrawingContext dc)
        {
            var monthlyUnits = parameters
                .GetMonthlyUnits()
                .ToList();

            int index = 0;
            for (int i = 0; i < monthlyUnits.Count; i++)
            {
                var originX = header.UnitWidth * index;
                DrawText(dc, monthlyUnits[i].MonthYear, originX, 0, weightedBlack);
                dc.DrawLine(majorTickPen, new Point(originX, 0), new Point(originX, header.TotalHeight));
                index = monthlyUnits[i].Index;
            }
        }

        private void AddHeaderOutlines(DrawingContext dc)
        {
            var stroke = new SolidColorBrush(Colors.Black);
            stroke.Freeze();
            var outlinePen = new Pen(stroke, 2);

            dc.DrawLine(outlinePen, new Point(0, header.TotalHeight),
                new Point(parameters.TotalWidth, header.TotalHeight));
        }

        internal void AddHeaderBackground(DrawingContext dc)
        {
            dc.DrawRectangle(header.Background, null, new Rect(0, 0, parameters.TotalWidth, header.UnitHeight));
        }

        private void AppendDayOfWeekBlock(DrawingContext dc, GanttDateUnit dateUnit, float originX)
        {
            var unitRect = new Rect(originX, header.UnitHeight, header.UnitWidth, 2 * header.UnitHeight);
            if (dateUnit.IsSaturday || dateUnit.IsSunday)
                dc.DrawRectangle(header.WeekendFill, minorTickPen, unitRect);
            else
                dc.DrawRectangle(null, minorTickPen, unitRect);

            if (dateUnit.IsSunday)
                dc.DrawLine(wkendTickPen, new Point(originX, header.UnitHeight), new Point(originX, header.TotalHeight));
        }

        private void AppendDayOfWeekText(DrawingContext dc, GanttDateUnit dateUnit, float originX)
        {
            var originY = 2 * header.UnitHeight;

            if (dateUnit.IsSaturday || dateUnit.IsSunday)
                DrawText(dc, dateUnit.WkDay, originX, originY, normalRed);
            else
                DrawText(dc, dateUnit.WkDay, originX, originY, normalBlack);
        }

        private void AppendNumberText(DrawingContext dc, GanttDateUnit dateUnit, float originX)
        {
            if (dateUnit.IsSaturday || dateUnit.IsSunday)
                DrawText(dc, dateUnit.Day.ToString(), originX, header.UnitHeight, weightedRed);
            else
                DrawText(dc, dateUnit.Day.ToString(), originX, header.UnitHeight, weightedBlack);
        }

        private void DrawText(DrawingContext dc, string textToFormat, double originX, double originY, Font font)
        {
            var text = new FormattedText(
                textToFormat,
                new CultureInfo("en-us"),
                FlowDirection.LeftToRight,
                font.GetTypeface(), font.Size, font.Foreground);

            if (text.Width < header.UnitWidth)
                originX += (header.UnitWidth - text.Width) / 2;
            else
                originX += 10;

            originY += (header.UnitHeight - text.Height) / 2;
            dc.DrawText(text, new Point(originX, originY));
        }
    }
}
