using System;

namespace Gantt.Shared
{
    public interface ITaskSchedule : IChangeTracking
    {
        string ItemName { get; }
        DateTime StartDate { get; set; }
        float Duration { get; set; }
        DateTime EndDate { get; set; }
        int RowIndex { get; }
        string StatusColor { get; set; }
        TaskStatus Status { get; }
        Action<ITaskSchedule> TaskSelected { get; set; }
        Action<IToolTipObject> ToolTipContentSetter { get; set; }
    }
}
