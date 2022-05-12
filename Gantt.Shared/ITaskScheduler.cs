using System;
using System.Collections.ObjectModel;

namespace Gantt.Shared
{
    public interface ITaskScheduler
    {
        string Name { get; }
        DateTime Start { get; }
        DateTime End { get; }
        ObservableCollection<ITaskSchedule> TaskSchedules { get; }
    }
}
