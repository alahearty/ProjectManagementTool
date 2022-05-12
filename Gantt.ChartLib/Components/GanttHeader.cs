using System.Windows.Media;

namespace Gantt.ChartLib.Components
{
    public class GanttHeader
    {
        public float UnitWidth { get; }
        public float UnitHeight { get; }
        public float TotalHeight { get; }
        public SolidColorBrush Foreground { get; }
        public SolidColorBrush Background { get; }
        public SolidColorBrush WeekendFill { get; }
        public SolidColorBrush WeekendForeground { get; }
        public string MinorStroke { get; set; }
        public string MajorStroke { get; set; }

        public GanttHeader()
        {
            UnitWidth = 25.0f;
            UnitHeight = 30.0f;
            TotalHeight = 3 * UnitHeight;
            MajorStroke = "#000000";
            MinorStroke = "#808080";

            Foreground = new SolidColorBrush(Colors.Black);
            Foreground.Freeze();

            WeekendFill = new SolidColorBrush(Colors.LightBlue);
            WeekendFill.Freeze();

            WeekendForeground = new SolidColorBrush(Colors.Red);
            WeekendForeground.Freeze();

            Background = new SolidColorBrush(Colors.LightCoral);
            Background.Freeze();
        }
    }
}
