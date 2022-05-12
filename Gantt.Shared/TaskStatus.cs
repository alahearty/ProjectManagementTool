using System.ComponentModel;

namespace Gantt.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class TaskStatus : INotifyPropertyChanged, IChangeTracking
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ColourCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Comments { get; set; }


        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
