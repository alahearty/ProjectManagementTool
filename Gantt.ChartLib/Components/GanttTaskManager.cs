using Gantt.ChartLib.Controls;
using Gantt.ChartLib.Utils;
using Gantt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace Gantt.ChartLib.Components
{
    public class GanttTaskManager
    {
        private GanttChartControl _gantt;
        private TaskStatus _overdue;

        public TaskStatusConfiguration TaskStatusConfig { get; private set; }

        public static GanttTaskManager Current { get; } = new GanttTaskManager();

        internal void Init(GanttChartControl gantt)
        {
            _gantt = gantt;
            TaskStatusConfig = new TaskStatusConfiguration();

            var timer = new Timer(60000);
            timer.Elapsed += (s, e) =>
                Application.Current.Dispatcher.Invoke(() => UpdateAllTasksStatus());
            timer.Start();
            UpdateAllTasksStatus();
        }

        public void SetConfigs(IEnumerable<TaskStatus> configs)
        {
            foreach (var status in configs)
                TaskStatusConfig.AddTaskStatus(status);
            UpdateAllTasksStatus();
        }

        public void UpdateAllTasksStatus()
        {
            if (_gantt == null || TaskStatusConfig == null) return;

            var pendingTasks = _gantt.TaskSchedules.Where(
                x => x.Status.Name == StatusKeys.NotStarted);

            _overdue = TaskStatusConfig.GetItems()
                    .First(x => x.Name == StatusKeys.Overdue);

            foreach (ITaskSchedule task in pendingTasks)
                UpdateTaskStatus(task);
        }

        public void UpdateTaskStatus(ITaskSchedule task)
        {
            if (task.EndDate.IsLessThan(DateTime.Now))
            {
                task.Status.Name = _overdue.Name;
                task.Status.ColourCode = _overdue.ColourCode;
                task.StatusColor = _overdue.ColourCode;
                task.OnChanged(nameof(task.StatusColor));
            }
        }
    }
}
