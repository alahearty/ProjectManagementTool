using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using Gantt.Shared;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    public class GanttTaskEditor : Canvas
    {
        private readonly GanttParameters parameters;
        private readonly VisualCollection visuals;

        public GanttTaskEditor(
            GanttParameters parameters)
        {
            ToolTip = new GanttToolTip();
            VerticalAlignment = VerticalAlignment.Top;
            this.parameters = parameters;
            visuals = new VisualCollection(this);

            PropertyBindings();
        }

        public IEnumerable<ITaskSchedule> TaskSchedules { get; private set; }
        public GanttTask SelectedTask { get; private set; }

        internal void InvalidateTasks(IEnumerable<ITaskSchedule> newTaskSchedules)
        {
            visuals.Clear();
            if (newTaskSchedules != null)
                foreach (var taskSchedule in newTaskSchedules)
                    visuals.Add(new GanttTask(taskSchedule, parameters));
            
            TaskSchedules = newTaskSchedules;
            GanttTaskManager.Current.UpdateAllTasksStatus();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            foreach (GanttTask visual in visuals)
                visual?.InvalidateVisual();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            var point = e.GetPosition((UIElement)e.Source);
            VisualTreeHelper.HitTest
                ((Visual)e.Source, null,
                new HitTestResultCallback(HitTestResultCallBack),
                new PointHitTestParameters(point));
            base.OnMouseDown(e);
        }

        protected override void OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs e)
        {         
            if (e.NewValue is true)
            {
                var point = Mouse.GetPosition(this);
                VisualTreeHelper.HitTest(this, null,
                    new HitTestResultCallback(OnHitSetToolTip),
                    new PointHitTestParameters(point));
            }
            base.OnIsMouseDirectlyOverChanged(e);
        }

        private HitTestResultBehavior OnHitSetToolTip(HitTestResult result)
        {
            if (result.VisualHit is GanttTask task)
            {
                task.SetToolTipContent((IToolTipObject)ToolTip);
                return HitTestResultBehavior.Stop;
            }
            return HitTestResultBehavior.Continue;
        }

        private HitTestResultBehavior HitTestResultCallBack(HitTestResult result)
        {
            if (result.VisualHit is GanttTask taskElement)
            {
                SelectedTask = taskElement;
                SelectedTask.InvokeSelection();
                return HitTestResultBehavior.Stop;
            }
            return HitTestResultBehavior.Continue;
        }

        internal void AddTask(ITaskSchedule taskSchedule)
        {
            var newTask = new GanttTask(taskSchedule, parameters);
            if (!visuals.Contains(newTask))
            {
                newTask.InvalidateVisual();
                visuals.Add(newTask);
            }
        }

        internal void RemoveTask(ITaskSchedule schedule)
        {      
            int index = 0;
            while (index < VisualChildrenCount)
            {
                var task = visuals[index] as GanttTask;
                if (task?.Schedule == schedule)
                {
                    visuals.Remove(task);
                    break;
                }
                index++;
            }
        }

        internal void ClearTasks()
        {
            visuals.Clear();    
        }

        protected virtual void PropertyBindings()
        {
            BindingBuilder binding =
                new BindingBuilder(this, HeightProperty, parameters.Grid, nameof(parameters.Grid.Height));
            binding.Append(BindingMode.TwoWay)
               .Append(UpdateSourceTrigger.PropertyChanged)
               .Bind();
        }

        protected override int VisualChildrenCount => visuals.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (visuals.Count == 0)
                return null;
            return visuals[index];
        }
    }
}
