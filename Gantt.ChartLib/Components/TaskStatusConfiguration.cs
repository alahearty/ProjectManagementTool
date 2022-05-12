using Gantt.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskStatus = Gantt.Shared.TaskStatus;

namespace Gantt.ChartLib.Components
{
    public class TaskStatusConfiguration 
    {
        private readonly ObservableCollection<TaskStatus> items;

        public TaskStatusConfiguration()
        {
            items = new ObservableCollection<TaskStatus>();

            AddTaskStatus(new TaskStatus()
            {
                Name = StatusKeys.NotStarted,
                ColourCode = "#c0c0c0"
            });

            AddTaskStatus(new TaskStatus()
            {
                Name = StatusKeys.Ongoing,
                ColourCode = "#ffbf00"
            });

            AddTaskStatus(new TaskStatus()
            {
                Name = StatusKeys.Suspended,
                ColourCode = "#ff0096"
            });

            AddTaskStatus(new TaskStatus()
            {
                Name = StatusKeys.Overdue,
                ColourCode = "#ff0000"
            });

            AddTaskStatus(new TaskStatus()
            {
                Name = StatusKeys.Completed,
                ColourCode = "#0000ff"
            });            
        }

        public IEnumerable<TaskStatus> GetItems()
        {
            return items;
        }

        public void AddTaskStatus(TaskStatus newStatus)
        {
            if (!items.Any(x => x.Name == newStatus.Name))
                items.Add(newStatus);
        }

        public void RemoveTaskStatus(TaskStatus taskStatus)
        {
            if (items.Any(x => x.Name == taskStatus.Name))
                items.Remove(taskStatus);
        }
    }
}
