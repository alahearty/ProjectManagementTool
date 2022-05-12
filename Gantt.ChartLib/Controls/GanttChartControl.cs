using Gantt.ChartLib.Components;
using Gantt.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gantt.ChartLib.Controls
{
    public class GanttChartControl : DockPanel
    {
        public ObservableCollection<ITaskSchedule> TaskSchedules
        {
            get { return (ObservableCollection<ITaskSchedule>)GetValue(TaskSchedulesProperty); }
            set { SetValue(TaskSchedulesProperty, value); }
        }
        public static readonly DependencyProperty TaskSchedulesProperty =
            DependencyProperty.Register("TaskSchedules", typeof(ObservableCollection<ITaskSchedule>),
                typeof(GanttChartControl), new PropertyMetadata(new ObservableCollection<ITaskSchedule>()));

        public DateTime StartDate
        {
            get { return (DateTime)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }
        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime),
                typeof(GanttChartControl), new PropertyMetadata(DateTime.Now));

        public DateTime EndDate
        {
            get { return (DateTime)GetValue(EndDateProperty); }
            set { SetValue(EndDateProperty, value); }
        }
        public static readonly DependencyProperty EndDateProperty =
            DependencyProperty.Register("EndDate", typeof(DateTime),
                typeof(GanttChartControl), new PropertyMetadata(DateTime.Now.AddMonths(6)));

        public GanttChartControl()
        {
            contentHost = new Grid();
            contentHost.RowDefinitions.Add(new RowDefinition());
            contentHost.ColumnDefinitions.Add(new ColumnDefinition());

            parameters = new GanttParameters();
            Header = new GanttHeaderCanvas(parameters);
            ganttGrid = new GanttGridCanvas(parameters);
            TaskEditor = new GanttTaskEditor(parameters);
            Tracker = new CurrentTimeIndicator(parameters);
            Loaded += (s, e) => GanttTaskManager.Current.Init(this);

            ConfigureViewports();
            HostGanttEditorComponents();
        }

        public GanttChartControl(
            IEnumerable<ITaskSchedule> taskSchedules,
            DateTime startDate,
            DateTime endDate)
            : this()
        {
            StartDate = startDate;
            EndDate = endDate;
            TaskSchedules = new ObservableCollection<ITaskSchedule>(taskSchedules);
        }

        protected virtual void OnCollectionChanged(
            object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                foreach (ITaskSchedule taskSchedule in e.NewItems)
                    TaskEditor.AddTask(taskSchedule);

            if (e.Action == NotifyCollectionChangedAction.Remove)
                foreach (ITaskSchedule taskSchedule in e.OldItems)
                    TaskEditor.RemoveTask(taskSchedule);

            if (e.Action == NotifyCollectionChangedAction.Reset)
                TaskEditor.ClearTasks();

            SetComponentHeight();
        }

        protected virtual void ConfigureViewports()
        {
            headerViewport = new ScrollViewer()
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Disabled
            };
            SetDock(headerViewport, Dock.Top);
            Children.Add(headerViewport);

            Viewport = new ScrollViewer()
            {
                HorizontalScrollBarVisibility = ScrollBarVisibility.Visible,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
            };
            Children.Add(Viewport);

            Viewport.ScrollChanged += (s, e) =>
                headerViewport.ScrollToHorizontalOffset(e.HorizontalOffset);
        }

        protected virtual void HostGanttEditorComponents()
        {
            Grid.SetRow(ganttGrid, 0);
            Grid.SetColumn(ganttGrid, 0);
            Grid.SetRow(TaskEditor, 0);
            Grid.SetColumn(TaskEditor, 0);
            Grid.SetRow(Tracker, 0);
            Grid.SetColumn(Tracker, 0);

            contentHost.Children.Add(ganttGrid);
            contentHost.Children.Add(TaskEditor);
            contentHost.Children.Add(Tracker);

            Viewport.Content = contentHost;
            headerViewport.Content = Header;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == TaskSchedulesProperty
                && e.NewValue != null)
            {
                SetComponentHeight();
                TaskEditor.InvalidateTasks(TaskSchedules);
                TaskEditor.InvalidateVisual();
                TaskSchedules.CollectionChanged += OnCollectionChanged;
            }

            if (e.Property == StartDateProperty 
                || e.Property == EndDateProperty)
            {
                SetComponentWidth();
                Header.InvalidateVisual();
                ganttGrid.InvalidateVisual();
                TaskEditor.InvalidateTasks(TaskSchedules);
                TaskEditor.InvalidateVisual();
            }
            base.OnPropertyChanged(e);
        }

        private void SetComponentWidth()
        {
            parameters.ComputeTimeUnits(StartDate, EndDate);
            var width = parameters.TotalWidth == 0 ? 1000 : parameters.TotalWidth;
            Header.Width = width;
            TaskEditor.Width = width;
            ganttGrid.Width = width;
        }

        private void SetComponentHeight()
        {
            if (TaskSchedules != null && TaskSchedules.Any())
            {
                var height = parameters.Grid.Spacing * TaskSchedules.Count;
                TaskEditor.Height = height;
            }
        }

        public GanttHeaderCanvas Header { get; }
        public GanttTaskEditor TaskEditor { get; }
        public ScrollViewer Viewport { get; private set; }
        public CurrentTimeIndicator Tracker { get; }

        private readonly GanttGridCanvas ganttGrid;
        private ScrollViewer headerViewport;
        private readonly Grid contentHost;
        private readonly GanttParameters parameters;
    }
}
