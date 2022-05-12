using Gantt.ChartLib.Components;
using Gantt.ChartLib.Utils;
using Gantt.Shared;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    /// <summary>
    /// Interaction logic for GanttDataGridControl.xaml
    /// </summary>
    public partial class GanttDataGridControl : Grid
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(GanttDataGridControl), new PropertyMetadata("Well Maintenance Schedules"));

        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize", typeof(double), typeof(GanttDataGridControl), new PropertyMetadata(16.0));

        public Brush TitleForeground
        {
            get { return (Brush)GetValue(TitleForegroundProperty); }
            set { SetValue(TitleForegroundProperty, value); }
        }
        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register("TitleForeground", typeof(Brush), typeof(GanttDataGridControl), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public static ICommand DateChangedCommand { get; } = new RoutedCommand();
        public static ICommand TextChangedCommand { get; } = new RoutedCommand();


        public GanttDataGridControl()
        {
            InitializeComponent();

            ContentGrid.CommandBindings.Add(new CommandBinding(DateChangedCommand, OnDateChanged));
            ContentGrid.CommandBindings.Add(new CommandBinding(TextChangedCommand, OnTextChanged));

            gridViewport.ScrollChanged += (s, e) =>
                headerViewport.ScrollToHorizontalOffset(e.HorizontalOffset);
        }

        public void SetVerticalScrollController(ScrollViewer controller)
        {
            controller.ScrollChanged += (s, e) =>
                gridViewport.ScrollToVerticalOffset(e.VerticalOffset);
        }

        private void OnTextChanged(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is ITaskSchedule task)
            {
                task.EndDate = task.StartDate.ToEndDate(task.Duration);
                task.OnChanged(nameof(task.EndDate));
                task.OnChanged(nameof(task.Duration));
                GanttTaskManager.Current.UpdateTaskStatus(task);
            }
        }

        private void OnDateChanged(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is ITaskSchedule task)
            {
                if(task.StartDate.IsGreaterThan(task.EndDate))
                {
                    task.EndDate = task.StartDate.ToEndDate(task.Duration);
                    task.OnChanged(nameof(task.StartDate));
                    task.OnChanged(nameof(task.EndDate));
                }
                else
                {
                    var duration = task.StartDate.ToDuration(task.EndDate);
                    if (duration == task.Duration)
                        return;
                    task.Duration = duration;
                    task.OnChanged(nameof(task.Duration));
                }
                GanttTaskManager.Current.UpdateTaskStatus(task);
            }
        }
    }
}
