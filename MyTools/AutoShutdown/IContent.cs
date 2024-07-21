namespace AutoShutdown
{
    public interface IContent
    {
        string Display { get; }
        string TimeCounter { get; }
        int ShutdownProgress { get; }
    }
}
