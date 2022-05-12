using Gantt.Shared;

namespace Gantt.ChartLib.Components
{
    public class GanttGrid : GanttItem, IChangeTracking
    {
        public float Height { get; set; } = 120;
        public float Spacing { get; set; } = 30;
        public float LineThickness { get; set; } = 1.0f;
        public string LineColor { get; set; } = "#808080";
        public bool ShowHorizontalGridLines { get; set; }
        public bool ShowVerticalGridLines { get; set; } = true;

        public void OnChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
