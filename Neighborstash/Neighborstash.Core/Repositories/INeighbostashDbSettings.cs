namespace Neighborstash.Core.Repositories
{
    public interface INeighbostashDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string HostName { get; set; }
        int PortNumber { get; set; }
    }
}