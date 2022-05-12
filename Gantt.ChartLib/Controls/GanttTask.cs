using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using Gantt.Shared;
using System;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    public class GanttTask : RenderingBase
    {     
        private readonly GanttParameters parameters;

        public GanttTask(
            ITaskSchedule taskSchedule,
            GanttParameters parameters)
        {          
            this.parameters = parameters;
            Schedule = taskSchedule;
            PropertyBindings();
            CanInvalidateVisual = true;
        }

        public ITaskSchedule Schedule { get; }

        public DateTime StartDate
        {
            get { return (DateTime)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }
        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime), typeof(GanttTask),
                new PropertyMetadata(DateTime.Now));

        public float Duration
        {
            get { return (float)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(float), typeof(GanttTask),
                new PropertyMetadata(0.0f));

        public string StatusCode
        {
            get { return (string)GetValue(StatusCodeProperty); }
            set { SetValue(StatusCodeProperty, value); }
        }
        public static readonly DependencyProperty StatusCodeProperty =
            DependencyProperty.Register("StatusCode", typeof(string), typeof(GanttTask),
                new PropertyMetadata("#C0C0C0"));

        public float Spacing
        {
            get { return (float)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(float), typeof(GanttTask),
                new PropertyMetadata(0.0f));

        public bool CanInvalidateVisual { get; }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (CanInvalidateVisual)
                InvalidateVisual();
            base.OnPropertyChanged(e);
        }

        protected override void CreateVisual(DrawingContext dc)
        {
            if (StatusCode == null)
                StatusCode = Colors.Transparent.ToString();

            var stroke = new SolidColorBrush(Colors.Black);
            stroke.Freeze();
            var drawingPen = new Pen(stroke, 1.0);
            drawingPen.Freeze();

            var statusCode = new BrushConverter()
                .ConvertFromString(StatusCode) as Brush;
            statusCode.Opacity = 1.0;
            statusCode?.Freeze();

            var startUnit = parameters.GetDateUnit(Schedule.StartDate);
            if (startUnit == null) return;

            var originX = parameters.Header.UnitWidth * startUnit.Index;
            var originY = Spacing * Schedule.RowIndex + 0.25 * Spacing;
            var width = parameters.Header.UnitWidth * Duration;
            var height = 0.5 * Spacing;

            var outline = new Rect(originX, originY, width, height);
            var cornerRadius = Spacing / 10;
            var geometry = new RectangleGeometry(outline, cornerRadius, cornerRadius);

            dc.DrawGeometry(statusCode, drawingPen, geometry);
            DrawText(dc, Schedule.ItemName, originX + width, Spacing * Schedule.RowIndex);
        }

        private void DrawText(DrawingContext dc, string textToFormat, double originX, double originY)
        {
            var text = new FormattedText(
                textToFormat,
                new CultureInfo("en-us"),
                FlowDirection.LeftToRight,
                parameters.TextFont.GetTypeface(),
                parameters.TextFont.Size,
                parameters.TextFont.Foreground);

            originX += 10;
            originY += (Spacing - text.Height) / 2;
            dc.DrawText(text, new Point(originX, originY));
        }

        internal void SetToolTipContent(IToolTipObject toolTip)
        {
            Schedule.ToolTipContentSetter?.Invoke(toolTip);
        }

        internal void InvokeSelection()
        {
            Schedule.TaskSelected?.Invoke(Schedule);
        }

        protected virtual void PropertyBindings()
        {
            BindingBuilder binding =
                new BindingBuilder(this, DurationProperty, Schedule, nameof(Schedule.Duration));
            binding.Append(BindingMode.OneWay)
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Bind();

            binding = new BindingBuilder(this, StartDateProperty, Schedule, nameof(Schedule.StartDate));
            binding.Append(BindingMode.OneWay) 
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Bind();

            binding =
               new BindingBuilder(this, SpacingProperty, parameters.Grid, nameof(parameters.Grid.Spacing));
            binding.Append(BindingMode.TwoWay)
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Bind();

            binding =
               new BindingBuilder(this, StatusCodeProperty, Schedule, nameof(Schedule.StatusColor));
            binding.Append(BindingMode.OneWay)
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Append(new StringToBrushConverter())
               .Bind();
        }
    }
}
