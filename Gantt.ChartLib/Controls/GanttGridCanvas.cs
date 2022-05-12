using Gantt.ChartLib.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    public class GanttGridCanvas : Canvas
    {
        private Pen drawingPen;
        private Brush wkendMarker;
        private readonly GanttParameters parameters;
        private readonly GanttHeader header;
        private readonly GanttGrid grid;
        private readonly ProjectTimeRegion activityRegion;

        public GanttGridCanvas(GanttParameters parameters)
        {
            this.parameters = parameters;
            header = parameters.Header;
            grid = parameters.Grid;
            activityRegion = new ProjectTimeRegion(parameters);
            InitDrawingPens();
        }

        private void InitDrawingPens()
        {
            drawingPen = GetPen();
            wkendMarker = header.WeekendFill.Clone();
            wkendMarker.Opacity = 0.15;
            wkendMarker.Freeze();
        }

        protected override void OnRender(DrawingContext dc)
        {
            SnapsToDevicePixels = true;
            VisualEdgeMode = EdgeMode.Aliased;

            if (grid.ShowVerticalGridLines)
                CreateVerticalGridlines(dc);

            if (grid.ShowHorizontalGridLines)
                CreateHorizontalGridlines(dc);

            activityRegion.DrawRegionBoundaries(dc, ActualWidth, ActualHeight);
        }

        private void CreateHorizontalGridlines(DrawingContext dc)
        {
            var y = grid.Spacing;
            while (y <= Height)
            {
                dc.DrawLine(drawingPen, new Point(0, y), new Point(ActualWidth, y));
                y += grid.Spacing;
            }
        }

        private void CreateVerticalGridlines(DrawingContext dc)
        {
            int counter = 0;
            foreach (var dateUnit in parameters.DateUnits)
            {
                var originX = counter * header.UnitWidth;
                var unitRect = new Rect(originX, 0, header.UnitWidth, ActualHeight);

                if (dateUnit.IsSaturday || dateUnit.IsSunday)
                    dc.DrawRectangle(wkendMarker, null, unitRect);
                dc.DrawLine(drawingPen, new Point(originX, 0), new Point(originX, ActualHeight));

                dateUnit.Index = counter;
                counter++;
            }
        }

        private Pen GetPen()
        {
            var stroke = new BrushConverter()
                .ConvertFromString(grid.LineColor) as Brush;
            stroke.Opacity = 0.35;
            stroke.Freeze();

            var newPen = new Pen(stroke, grid.LineThickness);
            newPen.Freeze();
            return newPen;
        }
    }
}
