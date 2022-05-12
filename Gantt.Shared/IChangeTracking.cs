namespace Gantt.Shared
{
    public interface IChangeTracking
    {
        void OnChanged(string propertyName);
    }
}
