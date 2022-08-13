namespace MediaManager.Services
{
    public interface IPageConfiguration
    {
        int DefaultPageSize { get; }
        int MaximumPageSize { get; }
    }
}