namespace Rubic.Caching.Configurations;

public class RubicDistributedCacheConfiguration
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; }
    public string InstanceName { get; set; }
}