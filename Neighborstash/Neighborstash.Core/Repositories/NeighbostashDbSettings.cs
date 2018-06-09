namespace Neighborstash.Core.Contracts
{
    public class NeighbostashDbSettings : INeighbostashDbSettings
    {
        public string HostName { get; set; }
        public int PortNumber { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}