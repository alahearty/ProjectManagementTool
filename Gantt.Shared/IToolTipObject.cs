namespace Gantt.Shared
{
    public interface IToolTipObject
    {
        object Content { get; set; }    
        double HorizontalOffset { get; set; }
        double VerticalOffset { get; set; }
        bool StaysOpen { get; set; }
    }
}
