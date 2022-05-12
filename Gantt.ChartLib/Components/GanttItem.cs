using System;
using System.ComponentModel;

namespace Gantt.ChartLib.Components
{
    public abstract class GanttItem : INotifyPropertyChanged
    {
        public virtual Guid Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
