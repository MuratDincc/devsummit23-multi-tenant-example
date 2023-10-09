namespace Rubic.Caching;

public static class CachingDefaults
{
    #region Properties

    /// <summary>
    /// Gets or sets a short cache time in minutes
    /// </summary>
    public static int ShortCacheTime { get; set; } = 1;

    /// <summary>
    /// Gets or sets a cache time in minutes
    /// </summary>
    public static int CacheTime { get; set; } = 120;

    #endregion
}
