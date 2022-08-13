namespace MediaManager.Services
{
    public interface IConfigurationRepository
    {
        IDatabaseConfiguration Database { get; }
        IApplicationConfiguration Application { get; }
    }
}